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
            var scssLines = new List<string>();

            var exports = new List<string>();

            if (Exists($"{directory}//index.ts")) 
                Delete($"{directory}//index.ts");

            if (Exists($"{directory}//index.scss"))
                Delete($"{directory}//index.scss");

            foreach (var directoryName in Directory.GetDirectories(directory)) 
                if (!IsDirectoryEmpty(directoryName) && GetFileName(directoryName) != "example")
                    lines.Add($"export * from \"./{GetFileName(directoryName)}\";");            

            foreach (var file in Directory.GetFiles(directory,"*.ts"))
                if (!file.Contains(".spec.ts"))
                    lines.Add($"export * from \"./{GetFileNameWithoutExtension(file)}\";");

            foreach (var file in Directory.GetFiles(directory, "*.scss"))
                scssLines.Add($"@import \"{GetFileNameWithoutExtension(file)}\";");

            _fileWriter.WriteAllLines($"{directory}//index.ts", lines);
            _fileWriter.WriteAllLines($"{directory}//index.scss", scssLines);
            try {
                _projectManager.Process(directory, "index.ts", FileType.TypeScript);
                _projectManager.Process(directory, "index.scss", FileType.Css);
            }
            catch { }

            return exitCode;
        }

        public bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
    }
}
