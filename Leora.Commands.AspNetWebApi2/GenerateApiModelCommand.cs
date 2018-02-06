using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateApiModelCommand: BaseCommand<GenerateApiModelOptions>, IGenerateApiModelCommand
    {
        public GenerateApiModelCommand(IFileWriter fileWriter,
            ITemplateManager templateManager,
            IDotNetTemplateProcessor templateProcessor,
            INamingConventionConverter namingConventionConverter,
            INamespaceManager namespaceManager,
            IProjectManager projectManager,
            ILeoraJSONFileManager leoraJSONFileManager)
            : base(fileWriter, templateManager, templateProcessor, namingConventionConverter, namespaceManager, projectManager,leoraJSONFileManager) { }

        public override int Run(GenerateApiModelOptions options) => Run(options.NameSpace, options.Directory, options.Name, options.RootNamespace,options.Framework);

        public int Run(string namespacename, string directory, string name, string rootNamespace, string framework)
        {
            int exitCode = 1;
            var pascalCaseName = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            var apiModelName = $"{pascalCaseName}ApiModel.cs";
            
            _fileWriter.WriteAllLines($"{directory}//{apiModelName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "ApiApiModel", BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));
            _projectManager.Process(directory, $"{apiModelName}", FileType.CSharp);

            return exitCode;
        }
    }
}
