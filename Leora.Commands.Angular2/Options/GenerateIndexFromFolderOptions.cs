using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.Commands.Angular2.Options
{
    public class GenerateIndexFromFolderOptions: BaseOptions
    {
        [Option("s", Required = false, HelpText = "Scss")]
        public bool IncludeScss { get; set; } = false;
    }
}
