using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateComponentCommand : BaseCommand<GenerateComponentOptions>, IGenerateComponentCommand
    {
        public GenerateComponentCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter)
            :base(templateManager,templateProcessor, namingConventionConverter,projectManager, fileWriter) { }

        public override int Run(GenerateComponentOptions options) => Run(options.Name, options.Directory, options.Framework);        

        public int Run(string name, string directory, bool framework = false)
        {

            int exitCode = 1;
            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var typeScriptFileName = $"{snakeCaseName}.component.ts";
            var typeScriptSpecFileName = $"{snakeCaseName}.component.spec.ts";
            var cssFileName = $"{snakeCaseName}.component.scss";
            var htmlFileName = $"{snakeCaseName}.component.html";
            var baseFilePath = $"{directory}//{snakeCaseName}";

            if (framework)
            {
                _fileWriter.WriteAllLines($"{baseFilePath}.component.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular1ComponentFramework", BluePrintType.Angular1), name));
                _fileWriter.WriteAllLines($"{baseFilePath}.component.spec.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular1ComponentSpec", BluePrintType.Angular1), name));
                _fileWriter.WriteAllLines($"{baseFilePath}.component.scss", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Css, "Angular1Component", BluePrintType.Angular1), name));
                _fileWriter.WriteAllLines($"{baseFilePath}.component.html", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Html, "Angular1Component", BluePrintType.Angular1), name));
                _projectManager.Process(directory, typeScriptFileName, FileType.TypeScript);
                _projectManager.Process(directory, typeScriptSpecFileName, FileType.TypeScript);
                _projectManager.Process(directory, cssFileName, FileType.Css);
                _projectManager.Process(directory, htmlFileName, FileType.Html);
            } else
            {
                _fileWriter.WriteAllLines($"{baseFilePath}.component.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular1Component", BluePrintType.Angular1), name));
                _fileWriter.WriteAllLines($"{baseFilePath}.component.spec.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular1ComponentSpec", BluePrintType.Angular1), name));
                _fileWriter.WriteAllLines($"{baseFilePath}.component.scss", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Css, "Angular1Component", BluePrintType.Angular1), name));
                _fileWriter.WriteAllLines($"{baseFilePath}.component.html", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Html, "Angular1Component", BluePrintType.Angular1), name));
                _projectManager.Process(directory, typeScriptFileName, FileType.TypeScript);
                _projectManager.Process(directory, typeScriptSpecFileName, FileType.TypeScript);
                _projectManager.Process(directory, cssFileName, FileType.Css);
                _projectManager.Process(directory, htmlFileName, FileType.Html);
            }
            return exitCode;
        }

    }
}
