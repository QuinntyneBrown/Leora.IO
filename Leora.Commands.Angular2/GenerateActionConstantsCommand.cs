using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Services.Contracts;
using Leora.Models;
using System.Collections.Generic;

namespace Leora.Commands.Angular2
{
    public class GenerateActionConstantsCommand: BaseCommand<GenerateActionConstantsOptions>, IGenerateActionConstantsCommand
    {
        protected readonly IGenerateIndexFromFolderCommand _generateIndexFromFolderCommand;

        public GenerateActionConstantsCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, GenerateIndexFromFolderCommand generateIndexFromFolderCommand) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) {
            _generateIndexFromFolderCommand = generateIndexFromFolderCommand;
        }

        public override int Run(GenerateActionConstantsOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var entityNameSnakeCase = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);

            var addTemplate = new List<string>() { "export const {{ entityNameAllCaps }}_ADD_SUCCESS = \"[{{ entityNamePascalCase }}] Add {{ entityNamePascalCase }} Success\";" }.ToArray();
            var getTemplate = new List<string>() { "export const {{ entityNameAllCaps }}_GET_SUCCESS = \"[{{ entityNamePascalCase }}] Get {{ entityNamePascalCase }} Success\";" }.ToArray();
            var removeTemplate = new List<string>() { "export const {{ entityNameAllCaps }}_REMOVE_SUCCESS = \"[{{ entityNamePascalCase }}] Remove {{ entityNamePascalCase }} Success\";" }.ToArray();
            var indexTemplate = new List<string>()
            {
                $"export * from \"./{entityNameSnakeCase}-add-success.action\";",
                $"export * from \"./{entityNameSnakeCase}-get-success.action\";",
                $"export * from \"./{entityNameSnakeCase}-remove-success.action\";"
            }.ToArray();
            
            _fileWriter.WriteAllLines($"{directory}//{entityNameSnakeCase}-add-success.action.ts", _templateProcessor.ProcessTemplate(addTemplate, name));
            _fileWriter.WriteAllLines($"{directory}//{entityNameSnakeCase}-get-success.action.ts", _templateProcessor.ProcessTemplate(getTemplate, name));
            _fileWriter.WriteAllLines($"{directory}//{entityNameSnakeCase}-remove-success.action.ts", _templateProcessor.ProcessTemplate(removeTemplate, name));

            try
            {
                _projectManager.Process(directory, $"{entityNameSnakeCase}-add-success.action.ts", FileType.TypeScript);
                _projectManager.Process(directory, $"{entityNameSnakeCase}-get-success.action.ts", FileType.TypeScript);
                _projectManager.Process(directory, $"{entityNameSnakeCase}-remove-success.action.ts", FileType.TypeScript);                
            }
            catch { }

            _generateIndexFromFolderCommand.Run(name, directory);

            return exitCode;
        }
    }
}
