using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Services.Contracts;
using Leora.Models;
using System.Collections.Generic;

namespace Leora.Commands.Angular2
{
    public class GenerateActionConstantsCommand: BaseCommand<GenerateActionConstantsOptions>, IGenerateActionConstantsCommand
    {
        public GenerateActionConstantsCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateActionConstantsOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var entityNameSnakeCase = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);

            var addTemplate = new List<string>() { "export const ADD_{{ entityNameAllCaps }}_SUCCESS = \"[{{ entityNamePascalCase }}] Add {{ entityNamePascalCase }} Success\";" }.ToArray();
            var getTemplate = new List<string>() { "export const GET_{{ entityNameAllCaps }}_SUCCESS = \"[{{ entityNamePascalCase }}] Get {{ entityNamePascalCase }} Success\";" }.ToArray();
            var removeTemplate = new List<string>() { "export const REMOVE_{{ entityNameAllCaps }}_SUCCESS = \"[{{ entityNamePascalCase }}] Remove {{ entityNamePascalCase }} Success\";" }.ToArray();
            var indexTemplate = new List<string>()
            {
                $"export * from \"./add-{entityNameSnakeCase}-success.action\";",
                $"export * from \"./get-{entityNameSnakeCase}-success.action\";",
                $"export * from \"./remove-{entityNameSnakeCase}-success.action\";"
            }.ToArray();
            
            _fileWriter.WriteAllLines($"{directory}//add-{entityNameSnakeCase}-success.action.ts", _templateProcessor.ProcessTemplate(addTemplate, name));
            _fileWriter.WriteAllLines($"{directory}//get-{entityNameSnakeCase}-success.action.ts", _templateProcessor.ProcessTemplate(getTemplate, name));
            _fileWriter.WriteAllLines($"{directory}//remove-{entityNameSnakeCase}-success.action.ts", _templateProcessor.ProcessTemplate(removeTemplate, name));
            _fileWriter.WriteAllLines($"{directory}//index.ts", _templateProcessor.ProcessTemplate(indexTemplate, name));


            try
            {
                _projectManager.Process(directory, $"add-{entityNameSnakeCase}-success.action.ts", FileType.TypeScript);
                _projectManager.Process(directory, $"get-{entityNameSnakeCase}-success.action.ts", FileType.TypeScript);
                _projectManager.Process(directory, $"remove-{entityNameSnakeCase}-success.action.ts", FileType.TypeScript);
                _projectManager.Process(directory, $"index.ts", FileType.TypeScript);
            }
            catch { }

            return exitCode;
        }
    }
}
