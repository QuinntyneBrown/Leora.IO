using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Models;
using Leora.Services.Contracts;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateConfigurationCommand: BaseCommand<GenerateConfigurationOptions>, IGenerateConfigurationCommand
    {
        public GenerateConfigurationCommand(IFileWriter fileWriter,
            ITemplateManager templateManager,
            IDotNetTemplateProcessor templateProcessor,
            INamingConventionConverter namingConventionConverter,
            INamespaceManager namespaceManager,
            IProjectManager projectManager)
            :base(fileWriter, templateManager,templateProcessor, namingConventionConverter, namespaceManager, projectManager) { }

        public override int Run(GenerateConfigurationOptions options) => Run(options.NameSpace, options.Directory, options.Name, options.RootNamespace);

        public int Run(string namespacename, string directory, string name, string rootNamespace)
        {
            int exitCode = 1;
            var pascalCaseName = $"{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}Configuration.cs";
            var interfacePascalCaseName = $"I{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}Configuration.cs";
            var entityNamePascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            var template = _templateManager.Get(FileType.CSharp, "ApiConfiguration", "Configuration", entityNamePascalCase, BluePrintType.AspNetWebApi2);
            var interfaceTemplate = _templateManager.Get(FileType.CSharp, "ApiIConfiguration", "Configuration", entityNamePascalCase, BluePrintType.AspNetWebApi2);
            _fileWriter.WriteAllLines($"{directory}//{pascalCaseName}", _templateProcessor.ProcessTemplate(template, name, namespacename, rootNamespace));
            _fileWriter.WriteAllLines($"{directory}//{interfacePascalCaseName}", _templateProcessor.ProcessTemplate(interfaceTemplate, name, namespacename, rootNamespace));

            try
            {
                _projectManager.Process(directory, $"{pascalCaseName}", FileType.CSharp);
                _projectManager.Process(directory, $"{interfacePascalCaseName}", FileType.CSharp);
            }
            catch {

            }
            return exitCode;
        }
    }
}
