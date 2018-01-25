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
            var cssFileName = $"{snakeCaseName}.component.css";

            var baseFilePath = $"{directory}//{snakeCaseName}";

            //var sufixList = new string[10] { "edit","list","item", "master-detail", "edit-embed", "paginated-list-embed", "list-embed", "item-embed", "overlay", "master-detail-embed" };
            var sufixList = new string[0] { };

            var templateTypescript = _templateManager.Get(FileType.TypeScript, "CustomElementsComponent", "Components", entityNamePascalCase, BluePrintType.CustomElements,sufixList);
            var templateScss = _templateManager.Get(FileType.Css, "CustomElementsComponent", "Components", entityNamePascalCase, BluePrintType.CustomElements, sufixList);
            
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

            _fileWriter.WriteAllLines($"{baseFilePath}.component.ts", _templateProcessor.ProcessTemplate(templateTypescript, name,prefix));
            _fileWriter.WriteAllLines($"{baseFilePath}.component.css", _templateProcessor.ProcessTemplate(templateScss, name, prefix));

            try
            {
                _projectManager.Process(directory, typeScriptFileName, FileType.TypeScript);
                _projectManager.Process(directory, cssFileName, FileType.Css);
            }
            catch  { }

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
