using System.IO;
using System.Linq;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;
using Leora.IO.ExtensionMethods;
using Leora.IO.TypeScript.AngularJS;
using Leora.IO.Paths;

namespace Leora.IO.FileSystemWatcher.Folders
{
    public class AppPathProcesser: IFileTriggeredProcesser
    {
        public void Process(EventType eventType, string fullPath)
        {
            if(eventType == EventType.Created && fullPath.IsFeatureFolder())
            {
                var featureName = fullPath.Split(System.IO.Path.DirectorySeparatorChar)[fullPath.Split(System.IO.Path.DirectorySeparatorChar).Count() - 1];

                // 1. Create componet editor, css html
                File.WriteAllLines(string.Format(fullPath + @"\{0}-editor.component.ts", featureName), new string[0]);
                File.WriteAllLines(string.Format(fullPath + @"\{0}-editor.component.html", featureName), new string[0]);
                File.WriteAllLines(string.Format(fullPath + @"\{0}-editor.component.css", featureName), new string[0]);

                // 2. Create componet list, css html
                File.WriteAllLines(string.Format(fullPath + @"\{0}-list.component.ts", featureName), new string[0]);
                File.WriteAllLines(string.Format(fullPath + @"\{0}-list.component.html", featureName), new string[0]);
                File.WriteAllLines(string.Format(fullPath + @"\{0}-list.component.css", featureName), new string[0]);

                // 3. Create actionCreator
                File.WriteAllLines(string.Format(fullPath + @"\{0}-actions.ts", featureName), new string[0]);

                // 4. Create service
                File.WriteAllLines(string.Format(fullPath + @"\{0}-service.ts", featureName), new string[0]);

                // 15. Create reducers
                File.WriteAllLines(string.Format(fullPath + @"\{0}-reducers.ts", featureName), new string[0]);

            }
        }
        public void xProcess(EventType eventType, string fullPath)
        {
            if (eventType == EventType.Created && fullPath.IsInsideAppFolder())
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
