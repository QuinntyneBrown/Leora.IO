using Leora.Commands.React.Contracts;
using Leora.Commands.React.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.React
{
    public class GenerateComponentCommand : BaseCommand<GenerateComponentOptions>, IGenerateComponentCommand
    {
        public GenerateComponentCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager) { }
        
        public override int Run(GenerateComponentOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            int exitCode = 1;

            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var typeScriptFileName = $"{snakeCaseName}.component.tsx";

            var baseFilePath = $"{directory}//{snakeCaseName}";
            
            WriteAllLines($"{baseFilePath}.component.tsx", _templateProcessor.ProcessTemplate(GetTemplate(FileType.TypeScript,"ReactComponent"), name));

            _projectManager.Process(directory, typeScriptFileName, FileType.TypeScript);

            return exitCode;
        }
    }
}
