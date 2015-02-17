using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.IO.Paths
{
    public static class AppPath
    {
        public static bool IsModuleFolder(string fullPath)
        {
            return (Directory.GetParent(fullPath).Name == "app" && Path.GetExtension(fullPath) == "");
        }
    }
}
