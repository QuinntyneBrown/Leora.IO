using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Angular1.Contracts;
using Leora.Services.Contracts;
using System;
using static System.IO.Directory;

namespace Leora.Commands.Angular1
{
    public class GenerateFeatureCommand : BaseCommand<GenerateFeatureOptions>, IGenerateFeatureCommand
    {
        protected readonly IGenerateModelCommand _generateModelCommand;
        protected readonly IGenerateModuleCommand _generateModuleCommand;
        protected readonly IGenerateContainerCommand _generateContainerCommand;
        protected readonly IGenerateComponentCommand _generateComponentCommand;
        protected readonly IGenerateActionCreatorCommand _generateActionCreatorCommand;
        protected readonly IGenerateServiceCommand _generateServiceCommand;
        protected readonly IGenerateListCommand _generateListCommand;
        protected readonly IGenerateEditorCommand _generateEditorCommand;

        protected readonly IMainManager _mainManager;

        public GenerateFeatureCommand(ITemplateManager templateManager, 
            ITemplateProcessor templateProcessor, 
            INamingConventionConverter namingConventionConverter, 
            IMainManager mainManager,
            IProjectManager projectManager,
            IFileWriter fileWriter,
            IGenerateModelCommand generateModelCommand,
            IGenerateModuleCommand generateModuleCommand,
            IGenerateComponentCommand generateComponentCommand,
            IGenerateContainerCommand generateContainerCommand,
            IGenerateActionCreatorCommand generateActionCreatorCommand,
            IGenerateServiceCommand generateServiceCommand,
            IGenerateListCommand generateListCommand,
            IGenerateEditorCommand generateEditorCommand
            )
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter)
        {
            _mainManager = mainManager;

            _generateModelCommand = generateModelCommand;
            _generateModuleCommand = generateModuleCommand;
            _generateContainerCommand = generateContainerCommand;
            _generateComponentCommand = generateComponentCommand;
            _generateActionCreatorCommand = generateActionCreatorCommand;
            _generateServiceCommand = generateServiceCommand;
            _generateListCommand = generateListCommand;
            _generateEditorCommand = generateEditorCommand;
        }

        public override int Run(GenerateFeatureOptions options) => Run(options.Name, options.Directory, options.Crud);

        public int Run(string name, string directory, bool crud)
        {
            var exitCode = 1;

            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var path = $"{directory}\\{snakeCaseName}";
            CreateDirectory(path);

            _generateModelCommand.Run(name, path);
            _generateModuleCommand.Run(name, path, crud);

            _generateContainerCommand.Run(name, path);
            _generateActionCreatorCommand.Run(name, path, crud);
            _generateComponentCommand.Run(name, path);

            if (crud)
            {
                _generateServiceCommand.Run(name, path, true, false);
                _generateListCommand.Run(name, path);
                _generateEditorCommand.Run(name, path);
            }

            _mainManager.AddToModules(name);
            _mainManager.AddToRoutes(name);

            return exitCode;
        }
    }
}
