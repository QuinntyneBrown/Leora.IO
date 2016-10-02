using Leora.Commands.Node.Contracts;
using Leora.Commands.Node.Options;
using Leora.Services.Contracts;
using System;

namespace Leora.Commands.Node
{
    public class GenerateJsConfigJsonCommand: BaseCommand<GenerateJsConfigJsonOptions>, IGenerateJsConfigJsonCommand
    {
        public GenerateJsConfigJsonCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter)
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter)
        {

        }

        public override int Run(GenerateJsConfigJsonOptions options) => Run();

        public int Run()
        {
            throw new NotImplementedException();
        }

    }
}
