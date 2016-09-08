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
    public class GenerateTypeDocJsonCommand: BaseCommand<GenerateTypeDocJsonOptions>, IGenerateTypeDocJsonCommand
    {
        public GenerateTypeDocJsonCommand(
            ITemplateManager templateManager, 
            ITemplateProcessor templateProcessor, 
            INamingConventionConverter namingConventionConverter, 
            IProjectManager projectManager, 
            IFileWriter fileWriter)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter) { }

        public override int Run(GenerateTypeDocJsonOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);

            _fileWriter.WriteAllLines($"{directory}\\typedoc.json", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Json, "Angular2TypeDocJson", BluePrintType.Angular2), name));

            try
            {
                _projectManager.Process(directory, "typedoc.json", FileType.Json);

            }
            catch (Exception e)
            {

            }

            return exitCode;
        }
    }
}
