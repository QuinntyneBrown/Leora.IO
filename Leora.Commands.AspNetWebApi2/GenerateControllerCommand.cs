using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateControllerCommand : BaseCommand<GenerateControllerOptions>, IGenerateControllerCommand
    {
        public GenerateControllerCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager)
            :base(templateManager,templateProcessor, namingConventionConverter, projectManager) { }

        public override int Run(GenerateControllerOptions options) => Run(options.NameSpace, options.Directory, options.Name);
        
        public int Run(string namespacename, string directory, string name)
        {
            int exitCode = 1;
            WriteAllLines($"{directory}//{name}Controller.cs", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular1Component"), name, namespacename));
            return exitCode;
        }
    }
}
