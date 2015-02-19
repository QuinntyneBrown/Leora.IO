using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.IO.Paths
{
    public static class SolutionPath
    {
        public static bool IsInsideSolutionPath(string fullPath)
        {
            var directories = fullPath.Split(System.IO.Path.DirectorySeparatorChar);

            return (directories[directories.Length - 3] == "app");
        }
    }
}
