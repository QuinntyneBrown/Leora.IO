using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Models;
using Leora.Services.Contracts;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateContentModelCommand : BaseCommand<GenerateContentModelOptions>, IGenerateContentModelCommand
    {
        public GenerateContentModelCommand(IFileWriter fileWriter, ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, INamespaceManager namespaceManager, IProjectManager projectManager, ILeoraJSONFileManager leoraJSONFileManager)
            :base(fileWriter,templateManager,templateProcessor, namingConventionConverter, namespaceManager, projectManager,leoraJSONFileManager) { }

        public override int Run(GenerateContentModelOptions options) => Run(options.NameSpace, options.Directory, options.Name, options.RootNamespace);
        
        public int Run(string namespacename, string directory, string name, string rootNamespace)
        {
            int exitCode = 1;
            var className = $"{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}ContentModel.cs";
            var interfaceName = $"I{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}ContentModel.cs";

            _fileWriter.WriteAllLines($"{directory}//{className}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "ApiContentModel", BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));
            _fileWriter.WriteAllLines($"{directory}//{interfaceName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "ApiIContentModel", BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));

            _projectManager.Process(directory, $"{className}", FileType.CSharp);
            _projectManager.Process(directory, $"{interfaceName}", FileType.CSharp);
            return exitCode;
        }
    }
}
