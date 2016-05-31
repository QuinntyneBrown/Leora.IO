using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;
using static System.IO.File;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateModelCommand : BaseCommand<GenerateModelOptions>, IGenerateModelCommand
    {
        public GenerateModelCommand(IFileWriter fileWriter, 
            ITemplateManager templateManager, 
            IDotNetTemplateProcessor templateProcessor, 
            INamingConventionConverter namingConventionConverter,
            INamespaceManager namespaceManager, 
            IProjectManager projectManager)
            :base(fileWriter, templateManager,templateProcessor, namingConventionConverter, namespaceManager, projectManager) { }

        public override int Run(GenerateModelOptions options) => Run(options.NameSpace, options.Directory, options.Name, options.RootNamespace);
        
        public int Run(string namespacename, string directory, string name, string rootNamespace)
        {
            int exitCode = 1;
            var pascalCaseName = $"{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}.cs";
            _fileWriter.WriteAllLines($"{directory}//{pascalCaseName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));
            _projectManager.Add(directory, $"{pascalCaseName}", FileType.CSharp);
            return exitCode;
        }
    }
}
