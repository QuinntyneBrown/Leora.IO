using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (System.IO.File.Exists($"{directory}//index.ts")) {
                System.IO.File.Delete($"{directory}//index.ts");
            }
            foreach(var file in System.IO.Directory.GetFiles(directory,"*.ts"))
            {
                if (!file.Contains(".spec.ts"))
                {
                    var fileName = System.IO.Path.GetFileName(file);
                    lines.Add($"export * from \"./{fileName}\"");
                }
            }

            _fileWriter.WriteAllLines($"{directory}//index.ts", lines.ToArray());

            try {
                _projectManager.Process(directory, "index.ts", FileType.TypeScript); }
            catch { }

            return exitCode;
        }
    }
}
