using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leora.IO.FileSystemWatcher.Contracts;
using Leora.IO.FileSystemWatcher.Enums;
using Leora.IO.ExtensionMethods;
using Leora.IO.Paths;
using Leora.IO.TypeScript.AngularJS;

namespace Leora.IO.FileSystemWatcher.Path
{
    public class ServicesPathProcessor: IFileTriggeredProcesser
    {
        public void Process(Enums.EventType eventType, string fullPath)
        {
            if (eventType == EventType.Created)
            {
                if (System.IO.Path.GetFileName(fullPath).Contains("RouteResolver.ts"))
                {
                    File.WriteAllLines(fullPath, RouteResolver.Get(fullPath.GetModuleName()));
                }

                if (System.IO.Path.GetFileName(fullPath).Contains("Service.ts"))                
                {
                    if (ModelsPath.Exists(fullPath))
                    {
                        File.WriteAllLines(fullPath,
                            Service.Get(fullPath.GetModuleName(),
                                System.IO.Path.GetFileNameWithoutExtension(fullPath)));
                    }
                    else
                    {
                        
                    }
                }                
            }
        }
    }
}
