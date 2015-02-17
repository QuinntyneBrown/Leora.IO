using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.IO.Paths
{
    public static class ModulePath
    {
        public static bool IsInsideModulePath(string fullPath)
        {
            var directories = fullPath.Split(System.IO.Path.DirectorySeparatorChar);

            return (directories[directories.Length - 3] == "app");
        }
    }
}
