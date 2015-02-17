using System;
using System.IO;
using Leora.IO.Configuration;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;
using Leora.IO.FileSystemWatcher.Providers;
using Microsoft.Practices.Unity;
using Leora.IO.Data.Contracts;

namespace Leora.IO.FileSystemWatcher
{
    class Program
    {
        private static IFileTriggerProcessersProvider fileTriggerProcessersProvider;

        static void Main(string[] args)
        {
            Console.WriteLine("Leora IO File System Watcher. Press \'q\' to quit.");

            RegisterComponents();

            fileTriggerProcessersProvider = new FileTriggeredProcessersProivder();

            System.IO.FileSystemWatcher watcher = new System.IO.FileSystemWatcher();
            watcher.Path = AppConfiguration.Config.ProjectFolderPath;
            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = true;
            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            while (Console.Read() != 'q');
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            foreach (var processer in fileTriggerProcessersProvider.Get())
            {
                processer.Process(EventType.Change, e.FullPath);
            }
        }

        private static void OnDeleted(object source, FileSystemEventArgs e)
        {
            foreach (var processer in fileTriggerProcessersProvider.Get())
            {
                processer.Process(EventType.Deleted, e.FullPath);
            }
        }

        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            foreach (var processer in fileTriggerProcessersProvider.Get())
            {
                processer.Process(EventType.Created, e.FullPath);
            }
        }

        private static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<ITemplateRepository, Leora.IO.Data.FileSystem.TemplateRepository>();
            UnityConfiguration.Container = container;
        }
    }
}
