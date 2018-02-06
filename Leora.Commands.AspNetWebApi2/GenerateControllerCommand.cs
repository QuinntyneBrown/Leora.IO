using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Models;
using Leora.Services.Contracts;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateControllerCommand : BaseCommand<GenerateControllerOptions>, IGenerateControllerCommand
    {
        public GenerateControllerCommand(ILeoraJSONFileManager leoraJSONFileManager,IFileWriter fileWriter, ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, INamespaceManager namespaceManager, IProjectManager projectManager)
            :base(fileWriter,templateManager,templateProcessor, namingConventionConverter, namespaceManager, projectManager, leoraJSONFileManager) { }

        public override int Run(GenerateControllerOptions options) => Run(options.NameSpace, options.Directory, options.Name, options.RootNamespace, options.Trace, options.CQRS);
        
        public int Run(string namespacename, string directory, string name, string rootNamespace, bool trace = false, bool cqrs = false)
        {
            int exitCode = 1;            
            var entityNamePascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            var pascalCaseName = $"{entityNamePascalCase}sController.cs";
            if (trace)
            {
                _fileWriter.WriteAllLines($"{directory}//{pascalCaseName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "ApiControllerTrace", BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));
            }
            else if(cqrs)
            {
                _fileWriter.WriteAllLines($"{directory}//{pascalCaseName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "ApiControllerCQRS", BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));
            }
            else
            {
                var template = _templateManager.Get(FileType.CSharp, "ApiController", "Controllers", entityNamePascalCase, BluePrintType.AspNetWebApi2);
                _fileWriter.WriteAllLines($"{directory}//{pascalCaseName}", _templateProcessor.ProcessTemplate(template, name, namespacename, rootNamespace));
            }
            
            _projectManager.Process(directory, $"{pascalCaseName}", FileType.CSharp);
            return exitCode;
        }
    }
}
