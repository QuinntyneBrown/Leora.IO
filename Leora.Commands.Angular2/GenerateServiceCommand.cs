using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.Commands.Angular2
{
    public class GenerateServiceCommand: BaseCommand<GenerateServiceOptions>, IGenerateServiceCommand
    {
        public GenerateServiceCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateServiceOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var entityNamePascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            var typeScriptFileName = entityNamePascalCase.Contains("Guard") ? $"{snakeCaseName}.ts" : $"{snakeCaseName}s.service.ts";

            var templateTypescript = _templateManager.Get(FileType.TypeScript, "Angular2Service", "Services", entityNamePascalCase, BluePrintType.Angular2);

            _fileWriter.WriteAllLines($"{directory}//{typeScriptFileName}", _templateProcessor.ProcessTemplate(templateTypescript, name));

            try
            {
                _projectManager.Process(directory, typeScriptFileName, FileType.TypeScript);
            }
            catch (Exception e)
            {

            }
            return exitCode;
        }
    }
}
