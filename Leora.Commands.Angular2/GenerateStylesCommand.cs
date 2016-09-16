using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System.Collections.Generic;

namespace Leora.Commands.Angular2
{
    public class GenerateStylesCommand: BaseCommand<GenerateStylesOptions>, IGenerateStylesCommand
    {
        
        public GenerateStylesCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) {
            
        }

        public override int Run(GenerateStylesOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var entityNameSnakeCase = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);

            _fileWriter.WriteAllLines($"{directory}//add-or-update.ts",
    _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2AddOrUpdate", BluePrintType.Angular2), name));


            var addTemplate = new List<string>() { "export const {{ entityNameAllCaps }}_ADD_SUCCESS = \"[{{ entityNamePascalCase }}] Add {{ entityNamePascalCase }} Success\";" }.ToArray();
            var getTemplate = new List<string>() { "export const {{ entityNameAllCaps }}_GET_SUCCESS = \"[{{ entityNamePascalCase }}] Get {{ entityNamePascalCase }} Success\";" }.ToArray();
            var removeTemplate = new List<string>() { "export const {{ entityNameAllCaps }}_REMOVE_SUCCESS = \"[{{ entityNamePascalCase }}] Remove {{ entityNamePascalCase }} Success\";" }.ToArray();

            _fileWriter.WriteAllLines($"{directory}//{entityNameSnakeCase}-add-success.action.ts", _templateProcessor.ProcessTemplate(addTemplate, name));

            try
            {
                _projectManager.Process(directory, $"{entityNameSnakeCase}-add-success.action.ts", FileType.TypeScript);
            }
            catch { }

            return exitCode;
        }
    }
}
