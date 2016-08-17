using System;
using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Services.Contracts;
using static CommandLine.Parser;
using Leora.Models;
using static System.IO.Directory;

namespace Leora.Commands.Angular2
{
    public class GenerateFeatureCommand : BaseCommand<GenerateFeatureOptions>, IGenerateFeatureCommand
    {
        protected readonly IGenerateIndexCommand _generateIndexCommand;
        protected readonly IGenerateBootstrapCommand _generateBootstrapCommand;
        protected readonly IGenerateModuleCommand _generateModuleCommand;
        protected readonly IGenerateComponentCommand _generateComponentCommand;
        
        public GenerateFeatureCommand(
            ITemplateManager templateManager, 
            ITemplateProcessor templateProcessor, 
            INamingConventionConverter namingConventionConverter, 
            IProjectManager projectManager, 
            IFileWriter fileWriter,
            IGenerateIndexCommand generateIndexCommand,
            IGenerateBootstrapCommand generateBootstrapCommand,
            IGenerateModuleCommand generateModuleCommand,
            IGenerateComponentCommand generateComponentCommand)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter) {

            _generateBootstrapCommand = generateBootstrapCommand;
            _generateComponentCommand = generateComponentCommand;
            _generateIndexCommand = generateIndexCommand;
            _generateModuleCommand = generateModuleCommand;
        }

        public override int Run(GenerateFeatureOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            int exitCode = 1;

            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var path = $"{directory}\\{snakeCaseName}";
            var examplesPath = $"{directory}\\{snakeCaseName}\\example";
            CreateDirectory(path);
            CreateDirectory(examplesPath);

            _generateComponentCommand.Run(name, path);
            _generateModuleCommand.Run(name, path);
            _generateBootstrapCommand.Run(name, examplesPath);
            _generateIndexCommand.Run(name, examplesPath);
            return exitCode;
        }

        
    }
}
