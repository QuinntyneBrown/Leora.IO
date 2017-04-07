using Leora.Services.Contracts;
using Leora.Models;
using CommandLine;
using Leora.Commands.Contracts;
using Leora.Commands.Angular2.Contracts;

namespace Leora.Commands.CustomElements
{
    public class GenerateFeatureOptions
    {
        public GenerateFeatureOptions()
        {
            Directory = System.Environment.CurrentDirectory;
            Prefix = "ce";
        }

        [Option('d', "directory", Required = false, HelpText = "Directory")]
        public string Directory { get; set; }

        [Option('n', "name", Required = true, HelpText = "Name")]
        public string Name { get; set; }

        [Option('p', "prefix", Required = false, HelpText = "Prefix")]
        public string Prefix { get; set; }
    }

    public interface IGenerateFeatureCommand : ICommand {        
        int Run(string name, string directory, string prefix);
    }

    public class GenerateFeatureCommand : BaseCommand<GenerateFeatureOptions>, IGenerateFeatureCommand
    {

        private readonly IGenerateCustomElementCommand _generateCustomElementCommand;

        private readonly IGenerateModelCommand _generateModelCommand;

        private readonly IGenerateActionsCommand _generateActionsCommand;

        private readonly IGenerateServiceCommand _generateServiceCommand;

        

        public GenerateFeatureCommand(
            ITemplateManager templateManager, 
            ICustomElementsTemplateProcessor templateProcessor, 
            INamingConventionConverter namingConventionConverter, 
            IProjectManager projectManager, 
            IFileWriter fileWriter, 
            IGenerateModelCommand generateModelCommand,
            IGenerateCustomElementCommand generateCustomElementCommand,
            IGenerateActionsCommand generateActionsCommand,
            IGenerateServiceCommand generateServiceCommand   
            )
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter) {
            _generateCustomElementCommand = generateCustomElementCommand;
            _generateActionsCommand = generateActionsCommand;
            _generateServiceCommand = generateServiceCommand;
            _generateModelCommand = generateModelCommand;
        }

        public override int Run(GenerateFeatureOptions options) => Run(options.Name, options.Directory, options.Prefix);

        public int Run(string name, string directory, string prefix)
        {
            _generateCustomElementCommand.Run($"{name}-edit-embed", directory, prefix);
            _generateCustomElementCommand.Run($"{name}-list-embed", directory, prefix);
            _generateCustomElementCommand.Run($"{name}-item-embed", directory, prefix);
            _generateCustomElementCommand.Run($"{name}-master-detail", directory, prefix);
            _generateActionsCommand.Run(name, directory, prefix);
            _generateServiceCommand.Run(name, directory, prefix);
            _generateModelCommand.Run(name, directory);
            return 0;
        }

    }
}
