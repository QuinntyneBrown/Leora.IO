using System.IO;
using System.Linq;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;
using Leora.IO.ExtensionMethods;
using Leora.IO.TypeScript.AngularJS;
using Leora.IO.Paths;
using Leora.IO.Configuration;

namespace Leora.IO.FileSystemWatcher.Folders
{
    public class AppPathProcesser: IFileTriggeredProcesser
    {
        public void Process(EventType eventType, string fullPath)
        {
            if ((eventType == EventType.Created || eventType == EventType.Change)
                && fullPath.IsFeatureFolder()
                && Directory.GetFiles(fullPath).Length == 0)
            {
                var entityName = fullPath.Split(System.IO.Path.DirectorySeparatorChar)[fullPath.Split(System.IO.Path.DirectorySeparatorChar).Count() - 1];
                File.WriteAllLines(string.Format(fullPath + @"\{0}.component.ts", entityName), TypeScript.Redux.Component.Get(entityName));
                File.WriteAllLines(string.Format(fullPath + @"\{0}.component.html", entityName), new string[0]);
                File.WriteAllLines(string.Format(fullPath + @"\{0}.component.css", entityName), new string[0]);


                if (AppConfiguration.Config.ReduxCrudMode)
                {
                    File.WriteAllLines(string.Format(fullPath + @"\{0}-editor.component.ts", entityName), TypeScript.Redux.Component.Editor(entityName));
                    File.WriteAllLines(string.Format(fullPath + @"\{0}-editor.component.html", entityName), new string[0]);
                    File.WriteAllLines(string.Format(fullPath + @"\{0}-editor.component.css", entityName), new string[0]);

                    File.WriteAllLines(string.Format(fullPath + @"\{0}-list.component.ts", entityName), TypeScript.Redux.Component.List(entityName));
                    File.WriteAllLines(string.Format(fullPath + @"\{0}-list.component.html", entityName), new string[0]);
                    File.WriteAllLines(string.Format(fullPath + @"\{0}-list.component.css", entityName), new string[0]);                    
                }

                File.WriteAllLines(string.Format(fullPath + @"\{0}.actions.ts", entityName), TypeScript.Redux.Actions.Get(entityName));

                File.WriteAllLines(string.Format(fullPath + @"\{0}.service.ts", entityName), TypeScript.Redux.Service.Get(entityName));

                File.WriteAllLines(string.Format(fullPath + @"\{0}.reducers.ts", entityName), TypeScript.Redux.Reducers.Get(entityName));

                File.WriteAllLines(string.Format(fullPath + @"\{0}.module.ts", entityName), TypeScript.Redux.Module.Get(entityName));


            }
        }
        public void xProcess(EventType eventType, string fullPath)
        {
            if (eventType == EventType.Created && fullPath.IsInsideAppFolder() && Directory.GetFiles(fullPath).Length == 0)
            {
                if(eventType == EventType.Created && AppPath.IsModuleFolder(fullPath))
                {

                    var moduleName = System.IO.Path.GetFileNameWithoutExtension(fullPath);

                    var moduleDirectory = fullPath.GetAppDirectory() + moduleName;

                    var componentsDirectory = moduleDirectory + @"\components";

                    var servicesDirectory = moduleDirectory + @"\services";

                    var interfacesDirectory = moduleDirectory + @"\interfaces";

                    var templatesDirectory = moduleDirectory + @"\templates";

                    //File.WriteAllLines(moduleDirectory + @"\default.html", new string[0]);

                    File.WriteAllLines(moduleDirectory + @"\module.ts", new string[0]);

                    Directory.CreateDirectory(componentsDirectory);

                    //File.WriteAllLines(string.Format(componentsDirectory + @"\{0}Editor.ts", moduleName), new string[0]);

                    //File.WriteAllLines(string.Format(componentsDirectory + @"\{0}List.ts", moduleName), new string[0]);

                    Directory.CreateDirectory(servicesDirectory);

                    //File.WriteAllLines(string.Format(servicesDirectory + @"\{0}RouteResolver.ts", moduleName), new string[0]);

                    //File.WriteAllLines(string.Format(servicesDirectory + @"\{0}Service.ts", moduleName), new string[0]);

                    Directory.CreateDirectory(interfacesDirectory);

                    //File.WriteAllLines(string.Format(interfacesDirectory + @"\I{0}Service.ts", moduleName), new string[0]);

                    Directory.CreateDirectory(templatesDirectory);

                    //File.WriteAllLines(templatesDirectory + @"\edit.html", new string[0]);

                    //File.WriteAllLines(templatesDirectory + @"\list.html", new string[0]);

                    //File.WriteAllLines(templatesDirectory + @"\index.html", new string[0]);

                }
            }
        }
    }
}
