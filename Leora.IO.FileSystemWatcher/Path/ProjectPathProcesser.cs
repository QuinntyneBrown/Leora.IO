using System;
using Leora.IO.CSharp.WebAPI;
using Leora.IO.ExtensionMethods;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;
using System.IO;

namespace Leora.IO.FileSystemWatcher.FolderWatchers
{
    public class ProjectFileProcessor : IFileTriggeredProcesser
    {
        public void Process(EventType eventType, string fullPath)
        {
            if (eventType == EventType.Created && fullPath == fullPath.GetProjectDirectory() + "Web.config")
            {
                File.WriteAllLines(fullPath.GetProjectDirectory() + "leora.txt", LeoraTxt.Get());

                File.WriteAllLines(fullPath, WebConfig.Get());
                
            }
        }
    }
}
