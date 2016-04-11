using System;
using System.IO;
using Leora.IO.Configuration;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;
using Leora.IO.FileSystemWatcher.Providers;
using Microsoft.Practices.Unity;
using Leora.IO.Data.Contracts;
using Leora.IO.ExtensionMethods;
using System.Dynamic;

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
            watcher.Renamed += OnChanged;

            string input;

            do
            {
                input = Console.ReadLine();

            } while (Console.Read() != 'q');
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            if (!e.FullPath.IsNewFolder())
            {
                foreach (var processer in fileTriggerProcessersProvider.Get())
                {
                    processer.Process(GetOptions(EventType.Change, e.FullPath));
                    
                }
                RemoveParams(e.FullPath);
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
            if (!e.FullPath.IsNewFolder())
            {
                foreach (var processer in fileTriggerProcessersProvider.Get())
                {
                    processer.Process(GetOptions(EventType.Change, e.FullPath));
                    
                }
                RemoveParams(e.FullPath);
            }
        }

        private static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<ITemplateRepository, Leora.IO.Data.FileSystem.TemplateRepository>();
            UnityConfiguration.Container = container;
        }

        private static dynamic GetOptions(EventType eventType, string fullPath)
        {
            var fileNameParts = System.IO.Path.GetFileNameWithoutExtension(fullPath).Split(new string[] { "--" }, StringSplitOptions.None);

            dynamic options = new ExpandoObject();
            options.eventType = eventType;
            options.fullPath = fullPath.Split(new string[] { "--" }, StringSplitOptions.None)[0];
            options.entityNameSnakeCase = System.IO.Path.GetFileNameWithoutExtension(options.fullPath);
            
            if (fileNameParts.Length > 1)
                for (var i = 1; i < fileNameParts.Length; i++)
                {
                    var optionParts = fileNameParts[i];
                    var opts = optionParts.Split(new string[] { "|" }, StringSplitOptions.None);
                    if (opts[0] == "crud" && opts.Length > 1)
                    {
                        options.crud = opts[1];
                    }
                    else if(opts[0] == "crud")
                    {
                        options.crud = null;
                    }

                    if (opts[0] == "simple" && opts.Length > 1)
                    {
                        options.simple = opts[1];
                    }
                    else if(opts[0] == "simple")
                    {
                        options.simple = null;
                    }
                }
            
            return options;
        }

        private static string RemoveParams(string fullPath)
        {
            if(!fullPath.IsNewFolder() && fullPath.Split(new string[] { "--" }, StringSplitOptions.None).Length > 1)
            {
                var newFullPath = fullPath.Split(new string[] { "--" }, StringSplitOptions.None)[0];

                if (!File.Exists(newFullPath) && File.Exists(fullPath))
                    Directory.Move(fullPath, newFullPath);

                return newFullPath;                
            }

            return fullPath;
        }        

    }
}
