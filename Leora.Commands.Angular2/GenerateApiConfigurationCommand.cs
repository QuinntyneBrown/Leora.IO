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
    public class GenerateApiConfigurationCommand: BaseCommand<GenerateApiConfigurationOptions>, IGenerateApiConfigurationCommand
    {
        public GenerateApiConfigurationCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateApiConfigurationOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var entityNameSnakeCase = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);

            _fileWriter.WriteAllLines($"{directory}//api-configuration.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2ApiConfiguration", BluePrintType.Angular2), name));

            try
            {
                _projectManager.Process(directory, "api-configuration.ts", FileType.TypeScript);
            }
            catch
            {

            }

            return exitCode;
        }
    }
}
