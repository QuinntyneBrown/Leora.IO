using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateControllerCommand : BaseCommand<GenerateControllerOptions>, IGenerateControllerCommand
    {
        public GenerateControllerCommand(ILeoraJSONFileManager leoraJSONFileManager,IFileWriter fileWriter, ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, INamespaceManager namespaceManager, IProjectManager projectManager)
            :base(fileWriter,templateManager,templateProcessor, namingConventionConverter, namespaceManager, projectManager, leoraJSONFileManager) { }

        public override int Run(GenerateControllerOptions options) => Run(options.NameSpace, options.Directory, options.Name, options.RootNamespace, options.Framework, options.Trace,  options.CQRS);
        
        public int Run(string namespacename, string directory, string name, string rootNamespace, string framework, bool trace = false,  bool cqrs = false)
        {
            Console.WriteLine($"{framework}");

            int exitCode = 1;            
            var entityNamePascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            var pascalCaseName = $"{entityNamePascalCase}sController.cs";
            if (trace)
            {
                _fileWriter.WriteAllLines($"{directory}//{pascalCaseName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "ApiControllerTrace", framework), name, namespacename, rootNamespace));
            }
            else if(cqrs)
            {
                _fileWriter.WriteAllLines($"{directory}//{pascalCaseName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "ApiControllerCQRS", framework), name, namespacename, rootNamespace));
            }
            else
            {
                var template = _templateManager.Get(FileType.CSharp, "ApiController", "Controllers", entityNamePascalCase, framework);
                _fileWriter.WriteAllLines($"{directory}//{pascalCaseName}", _templateProcessor.ProcessTemplate(template, name, namespacename, rootNamespace));
            }
            
            _projectManager.Process(directory, $"{pascalCaseName}", FileType.CSharp);
            return exitCode;
        }
    }
}
