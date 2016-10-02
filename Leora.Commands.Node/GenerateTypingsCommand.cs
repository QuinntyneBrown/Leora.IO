using System;
using Leora.Commands.Node.Contracts;
using Leora.Commands.Node.Options;
using Leora.Services.Contracts;

namespace Leora.Commands.Node
{
    public class GenerateTypingsCommand : BaseCommand<GenerateTypingsOptions>, IGenerateTypingsCommand
    {
        public GenerateTypingsCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter)
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter)
        {

        }

        public override int Run(GenerateTypingsOptions options) => Run();

        public int Run() {
            throw new NotImplementedException();
        }
    }
}
