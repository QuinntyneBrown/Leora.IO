using Leora.IO.Configuration;
using Leora.IO.ExtensionMethods;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Leora.IO.FileSystemWatcher.PostProcessers
{
    class ReduxAppStructureGenerator : IFileTriggeredProcesser
    {
        public void Process(EventType eventType, string fullPath)
        {
            if (fullPath.IsAppJavaScriptFile())
            {
                Console.WriteLine("Running App Structure Generator...");

                var outFile = fullPath.GetProjectDirectory() + "app-structure.txt";

                try
                {
                    if (File.Exists(outFile))
                    {
                        File.Delete(outFile);
                    }

                    var jsFiles = Directory.GetFiles(fullPath.GetAppDirectory(), "*.*", System.IO.SearchOption.AllDirectories).Where(x => System.IO.Path.GetExtension(x) == ".ts" && x.Contains("Spec.js") == false).ToList();

                    foreach (var file in jsFiles)
                    {
                        var fileName = file.Replace(file.GetAppDirectory(), @"app\");

                        if (!File.Exists(outFile))
                        {
                            File.WriteAllLines(outFile, new List<string>() { fileName });
                        }
                        else
                        {
                            File.AppendAllLines(outFile, new List<string>() { fileName });
                        }
                    }

                }
                catch (Exception exception)
                {

                    Console.WriteLine("Something went wrong...");
                }

                Console.WriteLine("Done!");
            }
        }
    }
}
