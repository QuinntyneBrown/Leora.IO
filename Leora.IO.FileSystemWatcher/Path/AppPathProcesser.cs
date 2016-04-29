using System.IO;
using System.Linq;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;
using Leora.IO.ExtensionMethods;
using Leora.IO.Configuration;
using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;
using System;

namespace Leora.IO.FileSystemWatcher.Folders
{
    public class AppPathProcesser: Leora.IO.FileSystemWatcher.Path.BaseProcessor
    {
        public override void Process(EventType eventType, string fullPath)
        {
            if ((eventType == EventType.Created || eventType == EventType.Change)
                && fullPath.IsFeatureFolder()
                && Directory.GetFiles(fullPath).Length == 0)
            {
                var entityName = fullPath.Split(System.IO.Path.DirectorySeparatorChar)[fullPath.Split(System.IO.Path.DirectorySeparatorChar).Count() - 1];
                File.WriteAllLines(string.Format(fullPath + @"\{0}.component.ts", entityName), TypeScript.Redux.Component.Get(new { entityNameSnakeCase = entityName }));
                File.WriteAllLines(string.Format(fullPath + @"\{0}.component.html", entityName), new string[0]);
                File.WriteAllLines(string.Format(fullPath + @"\{0}.component.css", entityName), new string[0]);


                if (AppConfiguration.Config.ReduxCrudMode)
                {
                    File.WriteAllLines(string.Format(fullPath + @"\{0}-editor.component.ts", entityName), TypeScript.Redux.Component.Editor(new { entityNameSnakeCase = entityName }));
                    File.WriteAllLines(string.Format(fullPath + @"\{0}-editor.component.html", entityName), new string[0]);
                    File.WriteAllLines(string.Format(fullPath + @"\{0}-editor.component.css", entityName), new string[0]);

                    File.WriteAllLines(string.Format(fullPath + @"\{0}-list.component.ts", entityName), TypeScript.Redux.Component.List(new { entityNameSnakeCase = entityName }));
                    File.WriteAllLines(string.Format(fullPath + @"\{0}-list.component.html", entityName), new string[0]);
                    File.WriteAllLines(string.Format(fullPath + @"\{0}-list.component.css", entityName), new string[0]);                    
                }

                File.WriteAllLines(string.Format(fullPath + @"\{0}.actions.ts", entityName), TypeScript.Redux.Actions.Get(new { entityNameSnakeCase = entityName }));

                File.WriteAllLines(string.Format(fullPath + @"\{0}.service.ts", entityName), TypeScript.Redux.Service.Get(new { entityNameSnakeCase = entityName }));

                File.WriteAllLines(string.Format(fullPath + @"\{0}.reducers.ts", entityName), TypeScript.Redux.Reducers.Get(new { entityNameSnakeCase = entityName }));

                File.WriteAllLines(string.Format(fullPath + @"\{0}.module.ts", entityName), TypeScript.Redux.Module.Get(new { entityNameSnakeCase = entityName }));


            }
        }

        public override void Process(EventType eventType, string fullPath, Dictionary<string, string> options)
        {
            Process(eventType, fullPath);
        }

        public override void Process(dynamic options)
        {
            string fullPath = options.fullPath;
            string entityNameSnakeCase = options.entityNameSnakeCase;
            EventType eventType = options.eventType;

            if ((eventType == EventType.Created || eventType == EventType.Change)
                && fullPath.IsFeatureFolder()
                && Directory.GetFiles(fullPath).Length == 0)
            {
                File.WriteAllLines(string.Format(fullPath + @"\{0}.component.ts", entityNameSnakeCase), TypeScript.Redux.Component.Get(options));
                File.WriteAllLines(string.Format(fullPath + @"\{0}.component.html", entityNameSnakeCase), new string[0]);
                File.WriteAllLines(string.Format(fullPath + @"\{0}.component.css", entityNameSnakeCase), new string[0]);

                try
                {
                    if (options.crud == null)
                    {
                        File.WriteAllLines(string.Format(fullPath + @"\{0}-editor.component.ts", entityNameSnakeCase), TypeScript.Redux.Component.Editor(options));
                        File.WriteAllLines(string.Format(fullPath + @"\{0}-editor.component.html", entityNameSnakeCase), TypeScript.Redux.Component.EditorHtml(options));
                        File.WriteAllLines(string.Format(fullPath + @"\{0}-editor.component.css", entityNameSnakeCase), new string[0]);

                        File.WriteAllLines(string.Format(fullPath + @"\{0}-list.component.ts", entityNameSnakeCase), TypeScript.Redux.Component.List(options));
                        File.WriteAllLines(string.Format(fullPath + @"\{0}-list.component.html", entityNameSnakeCase), TypeScript.Redux.Component.ListHtml(options));
                        File.WriteAllLines(string.Format(fullPath + @"\{0}-list.component.css", entityNameSnakeCase), TypeScript.Redux.Component.ListCss(options));

                        File.WriteAllLines(string.Format(fullPath + @"\{0}s-container.component.ts", entityNameSnakeCase), TypeScript.Redux.Component.Container(options));
                        File.WriteAllLines(string.Format(fullPath + @"\{0}s-container.component.html", entityNameSnakeCase), TypeScript.Redux.Component.ContainerHtml(options));
                        File.WriteAllLines(string.Format(fullPath + @"\{0}s-container.component.css", entityNameSnakeCase), new string[0]);
                    }
                }
                catch (RuntimeBinderException) { }

                try
                {
                    if (options.simple == null)
                    {
                        Console.WriteLine("Simple");
                    }
                }
                catch (RuntimeBinderException) { }

                File.WriteAllLines(string.Format(fullPath + @"\{0}.actions.ts", entityNameSnakeCase), TypeScript.Redux.Actions.Get(options));

                File.WriteAllLines(string.Format(fullPath + @"\{0}.service.ts", entityNameSnakeCase), TypeScript.Redux.Service.Get(options));

                File.WriteAllLines(string.Format(fullPath + @"\{0}.reducers.ts", entityNameSnakeCase), TypeScript.Redux.Reducers.Get(options));

                File.WriteAllLines(string.Format(fullPath + @"\{0}.module.ts", entityNameSnakeCase), TypeScript.Redux.Module.Get(options));

                File.WriteAllLines(string.Format(fullPath + @"\{0}.model.ts", entityNameSnakeCase), TypeScript.Redux.Model.Get(options));
            }

            if ((eventType == EventType.Created || eventType == EventType.Change) && fullPath.IsInsideControllerFolder())
            {

            }

            if ((eventType == EventType.Created || eventType == EventType.Change) && fullPath.IsInsideServiceFolder())
            {

            }

            if ((eventType == EventType.Created || eventType == EventType.Change) && fullPath.IsInsideDtoFolder())
            {

            }
        }
            
    }
}
