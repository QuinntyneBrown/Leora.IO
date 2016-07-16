using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateContainerCommand : BaseCommand<GenerateContainerOptions>, IGenerateContainerCommand
    {
        public GenerateContainerCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter)
            :base(templateManager,templateProcessor, namingConventionConverter,projectManager, fileWriter) { }

        public override int Run(GenerateContainerOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            int exitCode = 1;
            name = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var typeScriptFileName = $"{name}s-container.component.ts";
            var cssFileName = $"{name}s-container.component.scss";
            var htmlFileName = $"{name}s-container.component.html";

            _fileWriter.WriteAllLines($"{directory}\\{typeScriptFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, _componentName, BluePrintType.Angular1), name));
            _fileWriter.WriteAllLines($"{directory}\\{cssFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Css, _componentName, BluePrintType.Angular1), name));
            _fileWriter.WriteAllLines($"{directory}\\{htmlFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Html, _componentName, BluePrintType.Angular1), name));

            _projectManager.Process(directory, typeScriptFileName, FileType.TypeScript);
            _projectManager.Process(directory, cssFileName, FileType.Css);
            _projectManager.Process(directory, htmlFileName, FileType.Html);

            return exitCode;
        }

        private readonly string _componentName = "Angular1Container";
    }
}
