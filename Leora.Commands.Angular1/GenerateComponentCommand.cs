using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateComponentCommand : BaseCommand<GenerateComponentOptions>, IGenerateComponentCommand
    {
        public GenerateComponentCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter)
            :base(templateManager,templateProcessor, namingConventionConverter) { }
        
        public override int Run(GenerateComponentOptions options) => Run(options.Name, options.Directory);        

        public int Run(string name, string directory)
        {
            int exitCode = 1;
            var baseFilePath = $"{directory}//{_namingConventionConverter.Convert(NamingConvention.SnakeCase, name)}";
            WriteAllLines($"{baseFilePath}.component.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular1Component", "Angular1"), name));
            WriteAllLines($"{baseFilePath}.component.css", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Css, "Angular1Component", "Angular1"), name));
            WriteAllLines($"{baseFilePath}.component.html", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Html, "Angular1Component", "Angular1"), name));
            return exitCode;
        }

    }
}
