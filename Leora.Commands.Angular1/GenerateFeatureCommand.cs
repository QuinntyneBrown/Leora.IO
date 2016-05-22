using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
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


        public GenerateFeatureCommand(ITemplateManager templateManager, 
            ITemplateProcessor templateProcessor, 
            INamingConventionConverter namingConventionConverter, 
            IProjectManager projectManager,
            IGenerateModelCommand generateModelCommand,
            IGenerateModuleCommand generateModuleCommand,
            IGenerateComponentCommand generateComponentCommand,
            IGenerateContainerCommand generateContainerCommand,
            IGenerateActionCreatorCommand generateActionCreatorCommand,
            IGenerateServiceCommand generateServiceCommand,
            IGenerateListCommand generateListCommand,
            IGenerateEditorCommand generateEditorCommand
            )
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager)
        {
            _generateModelCommand = generateModelCommand;
            _generateModuleCommand = generateModuleCommand;
            _generateContainerCommand = generateContainerCommand;
            _generateComponentCommand = generateComponentCommand;
            _generateActionCreatorCommand = generateActionCreatorCommand;
            _generateServiceCommand = generateServiceCommand;
            _generateListCommand = generateListCommand;
            _generateEditorCommand = generateEditorCommand;
        }

        public override int Run(GenerateFeatureOptions options)
        {
            return Run(options.Name, options.Directory, options.Crud);
        }

        public int Run(string name, string directory, bool crud)
        {
            var exitCode = 1;

            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var newDirectory = $"{directory}\\{snakeCaseName}";
            CreateDirectory(newDirectory);

            _generateModelCommand.Run(name, newDirectory);
            _generateModuleCommand.Run(name, newDirectory, crud);
            _generateContainerCommand.Run(name, newDirectory);
            _generateActionCreatorCommand.Run(name, newDirectory, crud);
            _generateComponentCommand.Run(name, newDirectory);

            if (crud)
            {
                _generateServiceCommand.Run(name, newDirectory, true, false);
                _generateListCommand.Run(name, newDirectory);
                _generateEditorCommand.Run(name, newDirectory);
            }
            return exitCode;
        }
    }
}
