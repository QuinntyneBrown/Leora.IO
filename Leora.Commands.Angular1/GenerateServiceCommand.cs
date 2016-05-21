using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateServiceCommand : BaseCommand<GenerateServiceOptions>, IGenerateServiceCommand
    {
        private readonly INamingConventionConverter _namingConventionConverter;

        public GenerateServiceCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter)
            : base(templateManager, templateProcessor) {
            _namingConventionConverter = namingConventionConverter;
        }

        public override int Run(GenerateServiceOptions options)
        {
            int exitCode = 1;
            WriteAllLines($"{options.Directory}\\{ _namingConventionConverter.Convert(NamingConvention.SnakeCase,options.Name)}.service.ts", 
                _templateProcessor.ProcessTemplate(_templateManager.Get(Leora.Models.FileType.TypeScript, "Angular1Service"), options.Name));
            return exitCode;
        }
    }
}
