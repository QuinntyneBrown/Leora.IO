using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateNewCommand : BaseCommand<GenerateNewOptions>, IGenerateNewCommand
    {
        public GenerateNewCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, INamespaceManager namespaceManager, ILeoraJSONFileManager leoraJSONFileManager)
            :base(fileWriter,templateManager,templateProcessor,namingConventionConverter,namespaceManager,projectManager,leoraJSONFileManager) { }

        public override int Run(GenerateNewOptions options) => Run(options.Name, options.Directory);        

        public int Run(string name, string directory)
        {
            int exitCode = 1;
            

            return exitCode;
        }

    }
}
