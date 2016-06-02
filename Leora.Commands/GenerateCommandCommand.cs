using Leora.Commands.Contracts;
using Leora.Commands.Options;
using Leora.Services.Contracts;
using System;

namespace Leora.Commands
{
    public class GenerateCommandCommand : BaseCommand<GenerateCommandCommandOptions>, IGenerateCommandCommand
    {
        public GenerateCommandCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, INamespaceManager namespaceManager)
            :base(templateManager,templateProcessor, namingConventionConverter,projectManager,fileWriter, namespaceManager) { }

        public override int Run(GenerateCommandCommandOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;

            return 1;
        }
    }
}
