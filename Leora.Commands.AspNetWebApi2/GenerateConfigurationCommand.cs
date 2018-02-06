using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateConfigurationCommand: BaseCommand<GenerateConfigurationOptions>, IGenerateConfigurationCommand
    {
        private readonly IGenerateConfigCommand _generateConfigCommand;

        public GenerateConfigurationCommand(IFileWriter fileWriter,
            ITemplateManager templateManager,
            IDotNetTemplateProcessor templateProcessor,
            INamingConventionConverter namingConventionConverter,
            INamespaceManager namespaceManager,
            IProjectManager projectManager,
            IGenerateConfigCommand generateConfigCommand,
            ILeoraJSONFileManager leoraJSONFileManager
            )
            :base(fileWriter, templateManager,templateProcessor, namingConventionConverter, namespaceManager, projectManager,leoraJSONFileManager) {

            _generateConfigCommand = generateConfigCommand;

        }

        public override int Run(GenerateConfigurationOptions options) => Run(options.NameSpace, options.Directory, options.Name, options.RootNamespace, options.Framework);

        public int Run(string namespacename, string directory, string name, string rootNamespace, string framework)
        {
            int exitCode = 1;
            var pascalCaseName = $"{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}Configuration.cs";
            var interfacePascalCaseName = $"I{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}Configuration.cs";
            var entityNamePascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            var template = _templateManager.Get(FileType.CSharp, "ApiConfiguration", "Configuration", entityNamePascalCase, framework);

            _fileWriter.WriteAllLines($"{directory}//{pascalCaseName}", _templateProcessor.ProcessTemplate(template, name, namespacename, rootNamespace));

            _generateConfigCommand.Run(namespacename, System.IO.Path.GetDirectoryName(_projectManager.GetRelativePathAndProjFile(directory).ProjFile), name, rootNamespace,framework);

            try
            {
                _projectManager.Process(directory, $"{pascalCaseName}", FileType.CSharp);
                
            }
            catch {

            }
            return exitCode;
        }
    }
}
