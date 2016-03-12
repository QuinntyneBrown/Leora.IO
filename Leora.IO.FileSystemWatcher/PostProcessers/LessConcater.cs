using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.ExtensionMethods;
using Leora.IO.FileSystemWatcher.Enums;
using Leora.IO.Configuration;

namespace Leora.IO.FileSystemWatcher.PostProcessers
{
    public class LessConcater: Leora.IO.FileSystemWatcher.Path.BaseProcessor
    {
        public void Process(EventType eventType, string fullPath)
        {
            if (fullPath.IsAppLessFile())
            {
                Console.WriteLine("Running Less Concater...");

                var outFile = fullPath.GetProjectDirectory() + "app.less";

                try
                {
                    if (File.Exists(outFile))
                    {
                        File.Delete(outFile);
                    }

                    var lessFiles = Directory.GetFiles(fullPath.GetAppDirectory(), "*.*", System.IO.SearchOption.AllDirectories).Where(x => System.IO.Path.GetExtension(x) == ".less").ToList();

                    foreach (var file in lessFiles)
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
