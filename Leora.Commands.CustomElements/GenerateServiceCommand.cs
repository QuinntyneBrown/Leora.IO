using Leora.Services.Contracts;
using Leora.Models;
using CommandLine;
using Leora.Commands.Contracts;

namespace Leora.Commands.CustomElements
{
    public class GenerateServiceOptions
    {
        public GenerateServiceOptions()
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

    public interface IGenerateServiceCommand : ICommand
    {
        int Run(string name, string directory, string prefix);
    }

    public class GenerateServiceCommand : BaseCommand<GenerateServiceOptions>, IGenerateServiceCommand
    {
        public GenerateServiceCommand(ITemplateManager templateManager, ICustomElementsTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter) { }

        public override int Run(GenerateServiceOptions options) => Run(options.Name, options.Directory, options.Prefix);

        public int Run(string name, string directory, string prefix)
        {
            var exitCode = 1;
            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var entityNamePascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            var typeScriptFileName = $"{snakeCaseName}.service.ts";
            var baseFilePath = $"{directory}//{snakeCaseName}";

            var templateTypescript = _templateManager.Get(FileType.TypeScript, "CustomElementsService", "Services", entityNamePascalCase, BluePrintType.CustomElements);

            _fileWriter.WriteAllLines($"{snakeCaseName}.service.ts", _templateProcessor.ProcessTemplate(templateTypescript, name, prefix));

            try
            {
                _projectManager.Process(directory, typeScriptFileName, FileType.TypeScript);
            }
            catch { }

            return exitCode;
        }

    }
}
