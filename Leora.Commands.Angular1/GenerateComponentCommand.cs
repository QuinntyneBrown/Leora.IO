using Leora.Commands.Angular1.Contracts;
using Leora.Services.Contracts;
using static System.IO.File;
using Leora.Commands.Angular1.Options;
using Leora.Models;

namespace Leora.Commands.Angular1
{
    public class GenerateComponentCommand : BaseCommand<GenerateComponentOptions>, IGenerateComponentCommand
    {
        public GenerateComponentCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor)
            :base(templateManager,templateProcessor)
        {

        }
        
        public override int Run(GenerateComponentOptions options)
        {
            int exitCode = 1;
            WriteAllLines($"{options.Directory}/{options.Name}.component.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular1Component"), options.Name));
            WriteAllLines($"{options.Directory}/{options.Name}.component.css", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Css, "Angular1Component"), options.Name));
            WriteAllLines($"{options.Directory}/{options.Name}.component.html", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Html, "Angular1Component"), options.Name));
            return exitCode;
        }

    }
}
