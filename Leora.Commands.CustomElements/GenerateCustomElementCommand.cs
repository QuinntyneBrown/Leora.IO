using Leora.Services.Contracts;
using Leora.Models;
using CommandLine;
using Leora.Commands.Contracts;

namespace Leora.Commands.CustomElements
{
    public class GenerateCustomElementOptions
    {
        public GenerateCustomElementOptions()
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

    public interface IGenerateCustomElementCommand : ICommand {        
        int Run(string name, string directory, string prefix);
    }

    public class GenerateCustomElementCommand : BaseCommand<GenerateCustomElementOptions>, IGenerateCustomElementCommand
    {
        public GenerateCustomElementCommand(ITemplateManager templateManager, ICustomElementsTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter) { }

        public override int Run(GenerateCustomElementOptions options) => Run(options.Name, options.Directory, options.Prefix);

        public int Run(string name, string directory, string prefix)
        {
            var exitCode = 1;
            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var entityNamePascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            var typeScriptFileName = $"{snakeCaseName}.component.ts";
            var cssFileName = $"{snakeCaseName}.component.scss";
            var htmlFileName = $"{snakeCaseName}.component.html";
            var baseFilePath = $"{directory}//{snakeCaseName}";
            
            var templateTypescript = _templateManager.Get(FileType.TypeScript, "CustomElementsComponent", "Components", entityNamePascalCase, BluePrintType.CustomElements);
            var templateHtml = _templateManager.Get(FileType.Html, "CustomElementsComponent", "Components", entityNamePascalCase, BluePrintType.CustomElements);
            var templateScss = _templateManager.Get(FileType.Scss, "CustomElementsComponent", "Components", entityNamePascalCase, BluePrintType.CustomElements);
            
            _fileWriter.WriteAllLines($"{baseFilePath}.component.ts", _templateProcessor.ProcessTemplate(templateTypescript, name,prefix));
            _fileWriter.WriteAllLines($"{baseFilePath}.component.scss", _templateProcessor.ProcessTemplate(templateScss, name, prefix));
            _fileWriter.WriteAllLines($"{baseFilePath}.component.html", _templateProcessor.ProcessTemplate(templateHtml, name, prefix));

            try
            {
                _projectManager.Process(directory, typeScriptFileName, FileType.TypeScript);
                _projectManager.Process(directory, cssFileName, FileType.Css);
                _projectManager.Process(directory, htmlFileName, FileType.Html);
            }
            catch  { }

            return exitCode;
        }

    }
}
