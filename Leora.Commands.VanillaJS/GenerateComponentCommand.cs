using Leora.Services.Contracts;
using Leora.Models;
using CommandLine;
using Leora.Commands.Contracts;

namespace Leora.Commands.VanillaJS
{
    public class GenerateComponentOptions
    {
        public GenerateComponentOptions()
        {
            Directory = System.Environment.CurrentDirectory;
        }

        [Option('d', "directory", Required = false, HelpText = "Directory")]
        public string Directory { get; set; }

        [Option('n', "name", Required = true, HelpText = "Name")]
        public string Name { get; set; }
    }

    public interface IGenerateComponentCommand : ICommand
    {
        int Run(string name, string directory);
    }

    public class GenerateComponentCommand : BaseCommand<GenerateComponentOptions>, IGenerateComponentCommand
    {
        public GenerateComponentCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter) { }

        public override int Run(GenerateComponentOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var entityNamePascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            var typeScriptFileName = $"{snakeCaseName}.component.ts";
            var cssFileName = $"{snakeCaseName}.component.scss";
            var htmlFileName = $"{snakeCaseName}.component.html";
            var baseFilePath = $"{directory}//{snakeCaseName}";

            var sufixList = new string[4] { "edit", "list", "item", "overlay" };

            var templateTypescript = _templateManager.Get(FileType.TypeScript, "VanillaJSComponent", "Components", entityNamePascalCase, BluePrintType.VanillaJS, sufixList);
            var templateHtml = _templateManager.Get(FileType.Html, "VanillaJSComponent", "Components", entityNamePascalCase, BluePrintType.VanillaJS, sufixList);
            var templateScss = _templateManager.Get(FileType.Scss, "VanillaJSComponent", "Components", entityNamePascalCase, BluePrintType.VanillaJS, sufixList);

            foreach (var sufix in sufixList)
            {
                if (HasSufix(name, sufix))
                {
                    name = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
                    var newSufix = _namingConventionConverter.Convert(NamingConvention.PascalCase, sufix);
                    name = name.Substring(0, name.Length - newSufix.Length);
                    break;
                }
            }

            _fileWriter.WriteAllLines($"{baseFilePath}.component.ts", _templateProcessor.ProcessTemplate(templateTypescript, name));
            _fileWriter.WriteAllLines($"{baseFilePath}.component.scss", _templateProcessor.ProcessTemplate(templateScss, name));
            _fileWriter.WriteAllLines($"{baseFilePath}.component.html", _templateProcessor.ProcessTemplate(templateHtml, name));

            try
            {
                _projectManager.Process(directory, typeScriptFileName, FileType.TypeScript);
                _projectManager.Process(directory, cssFileName, FileType.Css);
                _projectManager.Process(directory, htmlFileName, FileType.Html);
            }
            catch { }

            return exitCode;
        }


        public bool HasSufix(string value, string sufix)
        {
            value = _namingConventionConverter.Convert(NamingConvention.PascalCase, value);
            sufix = _namingConventionConverter.Convert(NamingConvention.PascalCase, sufix);
            return value.EndsWith(sufix);
        }

        public string GetSufix(bool simple)
        {
            if (simple)
                return "css";

            return "scss";
        }

        public string ResolveComponentName(bool simple)
        {
            if (simple)
                return "Angular2SimpleComponent";

            return "Angular2Component";
        }

    }
}
