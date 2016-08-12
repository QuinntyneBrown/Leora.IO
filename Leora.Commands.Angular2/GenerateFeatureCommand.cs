using System;
using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Services.Contracts;
using static CommandLine.Parser;
using Leora.Models;
using static System.IO.Directory;

namespace Leora.Commands.Angular2
{
    public class GenerateFeatureCommand : IGenerateFeatureCommand
    {
        
        public GenerateFeatureCommand(
            IGenerateComponentCommand generateComponentCommand,
            ITemplateManager templateManager, 
            IDotNetTemplateProcessor templateProcessor, 
            INamingConventionConverter namingConventionConverter, 
            IProjectManager projectManager, 
            IFileWriter fileWriter, 
            INamespaceManager namespaceManager
            ) {
            _generateComponentCommand = generateComponentCommand;
            _namingConventionConverter = namingConventionConverter;
        }

        public int Run(string[] args)
        {
            throw new NotImplementedException();
        }

        public int Run(GenerateFeatureOptions options) => Run(options.Name, options.Directory);        

        public int Run(string name, string directory)
        {
            int exitCode = 1;

            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var path = $"{directory}\\{snakeCaseName}";
            var examplesPath = $"{directory}\\{snakeCaseName}\\examples";
            CreateDirectory(path);
            CreateDirectory(examplesPath);

            return exitCode;
        }

        public INamingConventionConverter _namingConventionConverter { get; set; }
        public IGenerateComponentCommand _generateComponentCommand { get; set; }
    }
}
