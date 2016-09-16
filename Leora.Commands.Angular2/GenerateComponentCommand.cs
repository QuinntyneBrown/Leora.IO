using System;
using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Services.Contracts;
using Leora.Models;
using System.Linq;

namespace Leora.Commands.Angular2
{
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
            var sufixList = new string[1] { "edit-page" };

            var templateTypescript = _templateManager.Get(FileType.TypeScript, "Angular2Component", "Components", entityNamePascalCase, BluePrintType.Angular2,sufixList);
            var templateHtml = _templateManager.Get(FileType.Html, "Angular2Component", "Components", entityNamePascalCase, BluePrintType.Angular2);
            var templateScss = _templateManager.Get(FileType.Scss, "Angular2Component", "Components", entityNamePascalCase, BluePrintType.Angular2);

            foreach(var sufix in sufixList)
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
            catch (Exception e)
            {

            }

            return exitCode;
        }

        public bool HasSufix(string value, string sufix)
        {
            value = _namingConventionConverter.Convert(NamingConvention.PascalCase, value);
            sufix = _namingConventionConverter.Convert(NamingConvention.PascalCase, sufix);
            return value.EndsWith(sufix);
        }
    }
}
