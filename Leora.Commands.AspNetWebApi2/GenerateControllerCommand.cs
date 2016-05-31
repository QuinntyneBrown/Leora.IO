using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateControllerCommand : BaseCommand<GenerateControllerOptions>, IGenerateControllerCommand
    {
        public GenerateControllerCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, INamespaceManager namespaceManager, IProjectManager projectManager)
            :base(templateManager,templateProcessor, namingConventionConverter, namespaceManager, projectManager) { }

        public override int Run(GenerateControllerOptions options) => Run(options.NameSpace, options.Directory, options.Name);
        
        public int Run(string namespacename, string directory, string name)
        {
            int exitCode = 1;
            var pascalCaseName = $"{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}Controller.cs";
            
            WriteAllLines($"{directory}//{pascalCaseName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, BluePrintType.AspNetWebApi2), name, namespacename));

            _projectManager.Add(directory, $"{pascalCaseName}", FileType.CSharp);

            return exitCode;
        }
    }
}
