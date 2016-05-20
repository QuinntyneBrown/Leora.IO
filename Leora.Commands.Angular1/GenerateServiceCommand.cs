using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateServiceCommand : BaseCommand<GenerateServiceOptions>, IGenerateServiceCommand
    {
        public GenerateServiceCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor)
            : base(templateManager, templateProcessor)
        { }

        public override int Run(GenerateServiceOptions options)
        {
            int exitCode = 1;
            WriteAllLines($"{options.Directory}/{options.Name}.service.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(Leora.Models.FileType.TypeScript, "Angular1Service"), options.Name));
            return exitCode;
        }
    }
}
