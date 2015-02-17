using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.IO.Paths
{
    public static class ComponentPath
    {
        public static bool IsComponentFolder(string fullPath)
        {
            return (Directory.GetParent(fullPath).Name == "components" && Path.GetExtension(fullPath) == "");
        }
    }
}
