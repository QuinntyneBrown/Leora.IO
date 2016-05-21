using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateComponentCommand : BaseCommand<GenerateComponentOptions>, IGenerateComponentCommand
    {
        public GenerateComponentCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager)
            :base(templateManager,templateProcessor, namingConventionConverter, projectManager) { }
        
        public override int Run(GenerateComponentOptions options) => Run(options.Name, options.Directory);        

        public int Run(string name, string directory)
        {
            int exitCode = 1;

            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var typeScriptFileName = $"{snakeCaseName}.component.ts";
            var cssFileName = $"{snakeCaseName}.component.css";
            var htmlFileName = $"{snakeCaseName}.component.html";
            var baseFilePath = $"{directory}//{snakeCaseName}";

            
            WriteAllLines($"{baseFilePath}.component.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular1Component", "Angular1"), name));
            WriteAllLines($"{baseFilePath}.component.css", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Css, "Angular1Component", "Angular1"), name));
            WriteAllLines($"{baseFilePath}.component.html", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Html, "Angular1Component", "Angular1"), name));

            _projectManager.Add(directory, typeScriptFileName, FileType.TypeScript);
            _projectManager.Add(directory, cssFileName, FileType.Css);
            _projectManager.Add(directory, htmlFileName, FileType.Html);

            return exitCode;
        }

    }
}
