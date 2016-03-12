﻿using System.Diagnostics;
using Leora.IO.Configuration;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leora.IO.ExtensionMethods;
using System.IO;

namespace Leora.IO.FileSystemWatcher.PostProcessers
{
    public class Gulp : Leora.IO.FileSystemWatcher.Path.BaseProcessor
    {
        public void Process(EventType eventType, string fullPath)
        {
            if(AppConfiguration.Config.GulpEnabled)
            {
                if (eventType == EventType.Change && File.Exists(fullPath.GetGulpFile()) && fullPath.GetProjectDirectory() + "app.js" == fullPath)
                {
                    Console.WriteLine("Running Gulp File...");
                    Console.WriteLine(DateTime.Now);
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = AppConfiguration.Config.GulpFilePath;
                    startInfo.Arguments = string.Format("--gulpfile {0}", fullPath.GetGulpFile());
                    System.Diagnostics.Process.Start(startInfo);                
                }                
            }

        }
    }
}
