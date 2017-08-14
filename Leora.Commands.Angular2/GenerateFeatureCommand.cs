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
        protected readonly IGenerateReadMeCommand _generateReadMeCommand;
        protected readonly IGeneratePackageJsonCommand _generatePackageJsonCommand;
        protected readonly IGenerateServiceCommand _generateServiceCommand;
        protected readonly IGenerateModelCommand _generateModelCommand;

        public GenerateFeatureCommand(
            ITemplateManager templateManager, 
            ITemplateProcessor templateProcessor, 
            INamingConventionConverter namingConventionConverter, 
            IProjectManager projectManager, 
            IFileWriter fileWriter,
            IGenerateIndexCommand generateIndexCommand,
            IGenerateBootstrapCommand generateBootstrapCommand,
            IGenerateModuleCommand generateModuleCommand,
            IGenerateComponentCommand generateComponentCommand,
            IGenerateReadMeCommand generateReadMeCommand,
            IGenerateServiceCommand generateServiceCommand,
            IGenerateModelCommand generateModelCommand
            )
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter) {

            _generateBootstrapCommand = generateBootstrapCommand;
            _generateComponentCommand = generateComponentCommand;
            _generateIndexCommand = generateIndexCommand;
            _generateModuleCommand = generateModuleCommand;
            _generateReadMeCommand = generateReadMeCommand;
            _generateServiceCommand = generateServiceCommand;
            _generateModelCommand = generateModelCommand;
        }

        public override int Run(GenerateFeatureOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            int exitCode = 1;

            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var path = $"{directory}\\{snakeCaseName}s";

            Console.WriteLine(path);

            CreateDirectory(path);

            _generateModelCommand.Run($"{name}", path);
            _generateComponentCommand.Run($"{name}-edit", path);
            _generateComponentCommand.Run($"{name}-paginated-list", path);
            _generateComponentCommand.Run($"{name}-list-item", path);
            _generateComponentCommand.Run($"{name}-paginated-list-page", path);
            _generateComponentCommand.Run($"{name}-edit-page", path);
            _generateComponentCommand.Run($"{name}s-left-nav", path);
            _generateServiceCommand.Run($"{name}", path);

            _generateModuleCommand.Run(name, path);

            return exitCode;
        }

        
    }
}
