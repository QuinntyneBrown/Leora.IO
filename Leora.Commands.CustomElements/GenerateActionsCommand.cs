using Leora.Services.Contracts;
using Leora.Models;
using CommandLine;
using Leora.Commands.Contracts;

namespace Leora.Commands.CustomElements
{
    public class GenerateActionsOptions
    {
        public GenerateActionsOptions()
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

    public interface IGenerateActionsCommand : ICommand {        
        int Run(string name, string directory, string prefix);
    }

    public class GenerateActionsCommand : BaseCommand<GenerateActionsOptions>, IGenerateActionsCommand
    {
        public GenerateActionsCommand(ITemplateManager templateManager, ICustomElementsTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter) { }

        public override int Run(GenerateActionsOptions options) => Run(options.Name, options.Directory, options.Prefix);

        public int Run(string name, string directory, string prefix)
        {
            var exitCode = 1;
            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var entityNamePascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            var typeScriptFileName = $"{snakeCaseName}.actions.ts";
            var baseFilePath = $"{directory}//{snakeCaseName}";
            
            var templateTypescript = _templateManager.Get(FileType.TypeScript, "CustomElementsActions", "Actions", entityNamePascalCase, BluePrintType.CustomElements);
            
            _fileWriter.WriteAllLines(typeScriptFileName, _templateProcessor.ProcessTemplate(templateTypescript, name,prefix));

            try
            {
                _projectManager.Process(directory, typeScriptFileName, FileType.TypeScript);
            }
            catch  { }

            return exitCode;
        }

    }
}
