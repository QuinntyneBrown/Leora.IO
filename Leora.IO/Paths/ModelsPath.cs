using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leora.IO.ExtensionMethods;

namespace Leora.IO.Paths
{
    public static class ModelsPath
    {

        public static bool Exists(string fullPath)
        {
            return Directory.Exists(fullPath.GetModelsDirectory());
        }

    }
}
