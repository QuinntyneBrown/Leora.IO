using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.Services
{
    public class RelativePathAndProjFile
    {
        public RelativePathAndProjFile(string fileName, string relativePath, string projFile)
        {
            RelativePath = $@"{relativePath}\{fileName}";
            ProjFile = projFile;
        }

        public string RelativePath { get; set; }
        public string ProjFile { get; set; }
    }
}
