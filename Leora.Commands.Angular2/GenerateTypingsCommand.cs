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
    public class GenerateTypingsCommand: BaseCommand<GenerateTypingsOptions>, IGenerateTypingsCommand
    {
        public GenerateTypingsCommand(
            ITemplateManager templateManager,
            ITemplateProcessor templateProcessor,
            INamingConventionConverter namingConventionConverter,
            IProjectManager projectManager,
            IFileWriter fileWriter)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter) { }

        public override int Run(GenerateTypingsOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);

            _fileWriter.WriteAllLines($"{directory}\\typings.json", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Json, "Angular2TypingsJson", BluePrintType.Angular2), name));

            try
            {
                _projectManager.Process(directory, "typings.json", FileType.Json);

            }
            catch (Exception e)
            {

            }

            return exitCode;
        }
    }
}
