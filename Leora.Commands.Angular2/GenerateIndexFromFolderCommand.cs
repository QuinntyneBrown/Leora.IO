using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.IO.File;
using System.IO;
using static System.IO.Path;

namespace Leora.Commands.Angular2
{
    public class GenerateIndexFromFolderCommand: BaseCommand<GenerateIndexFromFolderOptions>, IGenerateIndexFromFolderCommand
    {
        public GenerateIndexFromFolderCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateIndexFromFolderOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var lines = new List<string>();

            if (Exists($"{directory}//index.ts")) 
                Delete($"{directory}//index.ts");
            
            foreach(var file in Directory.GetFiles(directory,"*.ts"))
                if (!file.Contains(".spec.ts"))
                    lines.Add($"export * from \"./{GetFileNameWithoutExtension(file)}\";");
            
            _fileWriter.WriteAllLines($"{directory}//index.ts", lines);

            try {
                _projectManager.Process(directory, "index.ts", FileType.TypeScript);
            }
            catch { }

            return exitCode;
        }
    }
}
