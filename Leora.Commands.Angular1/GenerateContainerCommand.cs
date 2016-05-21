using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateContainerCommand : BaseCommand<GenerateContainerOptions>, IGenerateContainerCommand
    {
        public GenerateContainerCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager)
            :base(templateManager,templateProcessor,namingConventionConverter, projectManager) { }

        public override int Run(GenerateContainerOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            int exitCode = 1;

            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var typeScriptFileName = $"{ snakeCaseName }-container.component.ts";
            var htmlFileName = $"{snakeCaseName}-container.component.html";

            WriteAllLines($"{directory}/{ typeScriptFileName }.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, _componentName), name));
            WriteAllLines($"{directory}/{ htmlFileName }.html", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Html, _componentName), name));

            _projectManager.Add(directory, typeScriptFileName, FileType.TypeScript);
            _projectManager.Add(directory, htmlFileName, FileType.Html);

            return exitCode;
        }

        private readonly string _componentName = "Angular1ContainerComponent";

    }
}
