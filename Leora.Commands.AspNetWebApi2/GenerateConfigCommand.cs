using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateConfigCommand : BaseCommand<GenerateConfigOptions>, IGenerateConfigCommand
    {
        public GenerateConfigCommand(IFileWriter fileWriter,
            ITemplateManager templateManager,
            IDotNetTemplateProcessor templateProcessor,
            INamingConventionConverter namingConventionConverter,
            INamespaceManager namespaceManager,
            IProjectManager projectManager)
            : base(fileWriter, templateManager, templateProcessor, namingConventionConverter, namespaceManager, projectManager) { }

        public override int Run(GenerateConfigOptions options) => Run(options.NameSpace, options.Directory, options.Name, options.RootNamespace);

        public int Run(string namespacename, string directory, string name, string rootNamespace)
        {
            int exitCode = 1;
            var camelCaseName = _namingConventionConverter.Convert(NamingConvention.CamelCase, name);
            var configFileName = $"{camelCaseName}Configuration.config";

            _fileWriter.WriteAllLines($"{directory}//{configFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Config, "ApiConfig", BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));
            _projectManager.Process(directory, $"{configFileName}", FileType.Config);

            return exitCode;
        }
    }
}
