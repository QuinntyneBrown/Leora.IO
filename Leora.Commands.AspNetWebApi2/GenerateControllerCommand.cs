using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Models;
using Leora.Services.Contracts;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateControllerCommand : BaseCommand<GenerateControllerOptions>, IGenerateControllerCommand
    {
        public GenerateControllerCommand(IFileWriter fileWriter, ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, INamespaceManager namespaceManager, IProjectManager projectManager)
            :base(fileWriter,templateManager,templateProcessor, namingConventionConverter, namespaceManager, projectManager) { }

        public override int Run(GenerateControllerOptions options) => Run(options.NameSpace, options.Directory, options.Name, options.RootNamespace, options.Trace);
        
        public int Run(string namespacename, string directory, string name, string rootNamespace, bool trace = false)
        {
            int exitCode = 1;
            var pascalCaseName = $"{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}Controller.cs";

            if(trace)
            {
                _fileWriter.WriteAllLines($"{directory}//{pascalCaseName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "ApiControllerTrace", BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));
            }
            else
            {
                _fileWriter.WriteAllLines($"{directory}//{pascalCaseName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "ApiController", BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));
            }
            
            _projectManager.Process(directory, $"{pascalCaseName}", FileType.CSharp);
            return exitCode;
        }
    }
}
