using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leora.IO.Configuration;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;
using Leora.IO.ExtensionMethods;
using Leora.IO.TypeScript.AngularJS;
using Leora.IO.TypeScript.AngularJS.Jasmine;
using Leora.IO.Paths;

namespace Leora.IO.FileSystemWatcher.Path
{
    public class ComponentsPathProcessor: IFileTriggeredProcesser
    {
        public void Process(Enums.EventType eventType, string fullPath)
        {
            if (eventType == EventType.Created && fullPath.IsInsideComponentsFolder())
            {
                if (ComponentPath.IsComponentFolder(fullPath) && fullPath.IsDirectory())
                {
                    var componentName = System.IO.Path.GetFileNameWithoutExtension(fullPath);
                    var moduleName = fullPath.GetModuleName();
                    //Directory.CreateDirectory(fullPath + @"\interfaces");

                    if (AppConfiguration.Config.AutoResponsiveEnabled)
                    {
                        Directory.CreateDirectory(fullPath + @"\responsive less");
                        File.WriteAllLines(
                            string.Format(@"{0}\responsive less\{1}{2}", fullPath, componentName, ".less"),
                            new List<string>());
                        File.WriteAllLines(
                            string.Format(@"{0}\responsive less\{1}{2}", fullPath, componentName, ".xs.less"),
                            new List<string>());
                        File.WriteAllLines(
                            string.Format(@"{0}\responsive less\{1}{2}", fullPath, componentName, ".sm.less"),
                            new List<string>());
                        File.WriteAllLines(
                            string.Format(@"{0}\responsive less\{1}{2}", fullPath, componentName, ".md.less"),
                            new List<string>());
                        File.WriteAllLines(
                            string.Format(@"{0}\responsive less\{1}{2}", fullPath, componentName, ".lg.less"),
                            new List<string>());
                    }
                    else
                    {
                        File.WriteAllLines(
                            string.Format(@"{0}\{1}{2}", fullPath, componentName, ".less"),
                            new List<string>());

                    }

                    File.WriteAllLines(string.Format(@"{0}\{1}{2}", fullPath, componentName, ".html"),
                        new List<string>());

                    if (AppConfiguration.Config.CachedTemplatesEnabled)
                    {
                        File.WriteAllLines(string.Format(@"{0}\{1}{2}", fullPath, componentName, ".html.ts"),
                            new List<string>());
                    }

                    File.WriteAllLines(string.Format(@"{0}\{1}{2}", fullPath, componentName, ".ts"),
                        Component.Get(moduleName, componentName));

                    if (AppConfiguration.Config.AutoJasmineSpecsEnabled)
                    {
                        File.WriteAllLines(string.Format(@"{0}\{1}{2}", fullPath, componentName, "Spec.js"),
                            ComponentSpec.Get(moduleName, componentName));
                    }



                }
                else
                {
                    File.Delete(fullPath);

                    if (fullPath.IsGenerator())
                    {

                    }
                    else
                    {
                        var directory = fullPath.GetAppDirectory() +
                                        string.Format(@"\{0}\{1}", fullPath.GetModuleName(),
                                            System.IO.Path.GetFileNameWithoutExtension(fullPath));

                        var componentName = System.IO.Path.GetFileNameWithoutExtension(fullPath);
                        var moduleName = fullPath.GetModuleName();
                        Directory.CreateDirectory(directory);
                        Directory.CreateDirectory(directory + @"\interfaces");
                        Directory.CreateDirectory(directory + @"\responsive less");
                        File.WriteAllLines(string.Format(@"{0}\{1}{2}", directory, componentName, ".html"),
                            new List<string>());
                        File.WriteAllLines(string.Format(@"{0}\{1}{2}", directory, componentName, ".html.ts"),
                            new List<string>());
                        File.WriteAllLines(string.Format(@"{0}\{1}{2}", directory, componentName, ".ts"),
                            Component.Get(moduleName, componentName));
                        File.WriteAllLines(string.Format(@"{0}\{1}{2}", directory, componentName, "Spec.js"),
                            ComponentSpec.Get(moduleName, componentName));
                        File.WriteAllLines(
                            string.Format(@"{0}\responsive less\{1}{2}", directory, componentName, ".less"),
                            new List<string>());
                        File.WriteAllLines(
                            string.Format(@"{0}\responsive less\{1}{2}", directory, componentName, ".xs.less"),
                            new List<string>());
                        File.WriteAllLines(
                            string.Format(@"{0}\responsive less\{1}{2}", directory, componentName, ".sm.less"),
                            new List<string>());
                        File.WriteAllLines(
                            string.Format(@"{0}\responsive less\{1}{2}", directory, componentName, ".md.less"),
                            new List<string>());
                        File.WriteAllLines(
                            string.Format(@"{0}\responsive less\{1}{2}", directory, componentName, ".lg.less"),
                            new List<string>());
                    }
                }
            }
        }
    }
}
