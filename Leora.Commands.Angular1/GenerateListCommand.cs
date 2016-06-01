using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateListCommand : BaseCommand<GenerateListOptions>, IGenerateListCommand
    {

        public GenerateListCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter)
            :base(templateManager,templateProcessor, namingConventionConverter,projectManager, fileWriter) { }

        public override int Run(GenerateListOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            int exitCode = 1;
            name = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var typeScriptFileName = $"{name}-list.component.ts";
            var cssFileName = $"{name}-list.component.css";
            var htmlFileName = $"{name}-list.component.html";

            _fileWriter.WriteAllLines($"{directory}\\{typeScriptFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, _componentName, BluePrintType.Angular1), name));
            _fileWriter.WriteAllLines($"{directory}\\{cssFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Css, _componentName, BluePrintType.Angular1), name));
            _fileWriter.WriteAllLines($"{directory}\\{htmlFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Html, _componentName, BluePrintType.Angular1), name));

            _projectManager.Process(directory, typeScriptFileName, FileType.TypeScript);
            _projectManager.Process(directory, cssFileName, FileType.Css);
            _projectManager.Process(directory, htmlFileName, FileType.Html);

            return exitCode;
        }

        private readonly string _componentName = "Angular1ListComponent";
    }
}
