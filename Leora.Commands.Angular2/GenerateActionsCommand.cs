using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System.Collections.Generic;

namespace Leora.Commands.Angular2
{
    public class GenerateActionsCommand: BaseCommand<GenerateActionsOptions>, IGenerateActionsCommand
    {
        public GenerateActionsCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateActionsOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var entityNameSnakeCase = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var entityNamePascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            var template = _templateManager.Get(FileType.TypeScript, "Angular2Actions", "Actions", entityNamePascalCase, BluePrintType.Angular2);

            _fileWriter.WriteAllLines($"{directory}//{entityNameSnakeCase}.actions.ts", _templateProcessor.ProcessTemplate(template, name));
            
            try
            {
                _projectManager.Process(directory, $"{entityNameSnakeCase}.actions.ts", FileType.TypeScript);
            }
            catch {

            }

            return exitCode;
        }
    }
}
