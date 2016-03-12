using System.IO;
using Leora.IO.Configuration;
using Leora.IO.ExtensionMethods;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.IO.FileSystemWatcher.PostProcessers
{
    public class JasmineSpecConcater : Leora.IO.FileSystemWatcher.Path.BaseProcessor
    {
        public void Process(EventType eventType, string fullPath)
        {
            if (fullPath.IsAppJavaScriptSpecFile())
            {
                Console.WriteLine("Running Jasmine Spec Concater...");

                var outFile = fullPath.GetProjectDirectory() + "tests.js";

                try
                {
                    if (File.Exists(outFile))
                    {
                        File.Delete(outFile);
                    }

                    var jsFiles = Directory.GetFiles(fullPath.GetAppDirectory(), "*.*", System.IO.SearchOption.AllDirectories).Where(x => System.IO.Path.GetExtension(x) == ".js" && x.Contains("Spec.js") == true).ToList();

                    foreach (var file in jsFiles)
                    {
                        if (!File.Exists(outFile))
                        {
                            File.WriteAllLines(outFile, File.ReadAllLines(file));
                        }
                        else
                        {
                            File.AppendAllLines(outFile, File.ReadAllLines(file));
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
