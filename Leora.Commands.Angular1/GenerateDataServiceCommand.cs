using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateDataServiceCommand : BaseCommand<GenerateDataServiceOptions>, IGenerateComponentCommand
    {
        public GenerateDataServiceCommand(ITemplateProcessor templateProcessor, ITemplateManager templateManager)
            :base(templateManager, templateProcessor) { }

        public override int Run(GenerateDataServiceOptions options)
        {
            return Run(options.Name, options.Directory);
        }

        public int Run(string name, string directory)
        {
            int exitCode = 1;
            WriteAllLines($"{directory}/{name}.service.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(Leora.Models.FileType.TypeScript, "Angular1DataService"), name));
            return exitCode;
        }
    }
}
