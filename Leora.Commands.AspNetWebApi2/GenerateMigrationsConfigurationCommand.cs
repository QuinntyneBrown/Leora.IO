using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Models;
using Leora.Services.Contracts;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateMigrationsConfigurationCommand : BaseCommand<GenerateMigrationsConfigurationOptions>, IGenerateMigrationsConfigurationCommand
    {
        public GenerateMigrationsConfigurationCommand(ILeoraJSONFileManager leoraJSONFileManager,IFileWriter fileWriter, ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, INamespaceManager namespaceManager, IProjectManager projectManager)
            : base(fileWriter, templateManager, templateProcessor, namingConventionConverter, namespaceManager, projectManager,leoraJSONFileManager) { }

        public override int Run(GenerateMigrationsConfigurationOptions options) => Run(options.NameSpace, options.Directory, options.Name, options.RootNamespace);

        public int Run(string namespacename, string directory, string name, string rootNamespace)
        {
            int exitCode = 1;
            var pascalCaseName = $"{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}Configuration.cs";
            _fileWriter.WriteAllLines($"{directory}//{pascalCaseName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "ApiMigrationConfiguration", BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));
            _projectManager.Process(directory, $"{pascalCaseName}", FileType.CSharp);
            return exitCode;
        }
    }
}
