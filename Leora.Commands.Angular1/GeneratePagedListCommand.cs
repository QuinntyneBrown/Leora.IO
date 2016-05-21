using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GeneratePagedListCommand : BaseCommand<GeneratePagedListOptions>, IGeneratePagedListCommand
    {
        protected readonly INamingConventionConverter _namingConventionConverter;

        public GeneratePagedListCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter)
            :base(templateManager,templateProcessor) {
            _namingConventionConverter = namingConventionConverter;
        }

        public override int Run(GeneratePagedListOptions options) => Run(options.Directory, options.Name);

        public int Run(string directory, string name)
        {
            int exitCode = 1;
            name = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            WriteAllLines($"{directory}\\{name}-paged-list.component.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, _componentName, "Angular1"), name));
            WriteAllLines($"{directory}\\{name}-paged-list.component.css", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Css, _componentName, "Angular1"), name));
            WriteAllLines($"{directory}\\{name}-paged-list.component.html", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Html, _componentName, "Angular1"), name));
            return exitCode;
        }

        private readonly string _componentName = "Angular1PagedListComponent";
    }
}
