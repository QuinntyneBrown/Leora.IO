using Leora.Commands.Contracts;
using Leora.Commands.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands
{
    public class GenerateWebComponentsCommand : BaseCommand<GenerateWebComponentsOptions>, IGenerateWebComponentsCommand
    {
        public GenerateWebComponentsCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, INamespaceManager namespaceManager)
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter,namespaceManager) {
        }
        public override int Run(GenerateWebComponentsOptions options) => Run(options.Name, options.Directory);        

        public int Run(string name, string directory)
        {
            int exitCode = 1;
            
            return exitCode;
        }

    }
}
