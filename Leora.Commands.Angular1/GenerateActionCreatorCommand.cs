using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Services.Contracts;
using static System.IO.File;
using Leora.Models;

namespace Leora.Commands.Angular1
{
    public class GenerateActionCreatorCommand : BaseCommand<GenerateActionCreatorOptions>, IGenerateActionCreatorCommand
    {
        public GenerateActionCreatorCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor)
            :base(templateManager,templateProcessor) { }

        public override int Run(GenerateActionCreatorOptions options)
        {
            int exitCode = 1;
            WriteAllLines($"{options.Directory}/{options.Name}.component.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular1ActionCreator"), options.Name));
            return exitCode;
        }
    }
}
