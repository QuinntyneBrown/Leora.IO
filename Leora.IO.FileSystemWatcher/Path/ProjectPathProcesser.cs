using System;
using Leora.IO.CSharp.WebAPI;
using Leora.IO.ExtensionMethods;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;
using System.IO;
using System.Collections.Generic;
using Leora.IO.FileSystemWatcher.Path;

namespace Leora.IO.FileSystemWatcher.FolderWatchers
{
    public class ProjectFileProcessor : Leora.IO.FileSystemWatcher.Path.BaseProcessor
    {
        public void Process(EventType eventType, string fullPath)
        {
            if (eventType == EventType.Created && fullPath == fullPath.GetProjectDirectory() + "Web.config")
            {
                File.WriteAllLines(fullPath.GetProjectDirectory() + "leora.txt", LeoraTxt.Get());

                File.WriteAllLines(fullPath, WebConfig.Get());
                
            }
        }

        public void Process(EventType eventType, string fullPath, Dictionary<string, string> options)
        {
            throw new NotImplementedException();
        }
    }
}
