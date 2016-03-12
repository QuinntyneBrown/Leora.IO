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
    public class ModulePathProcessor: Leora.IO.FileSystemWatcher.Path.BaseProcessor
    {
        public override void Process(EventType eventType, string fullPath)
        {
            if (eventType == EventType.Created && ModulePath.IsInsideModulePath(fullPath))
            {
                if (System.IO.Path.GetFileName(fullPath) == "module.ts")
                {
                    File.WriteAllLines(fullPath, Module.Get(fullPath.GetModuleName(), Directory.Exists(fullPath.GetAppDirectory() + "core")));
                }

                if (System.IO.Path.GetFileName(fullPath) == "default.html")
                {
                    File.WriteAllLines(fullPath, DefaultHtml.Get(fullPath.GetModuleName()));
                }                
            }
        }

        public override void Process(EventType eventType, string fullPath, Dictionary<string, string> options)
        {
            throw new NotImplementedException();
        }
    }
}
