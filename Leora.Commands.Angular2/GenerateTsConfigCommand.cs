using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;

namespace Leora.Commands.Angular2
{
    public class GenerateTsConfigCommand: BaseCommand<GenerateTsConfigJsonOptions>, IGenerateTsConfigJsonCommand
    {
        public GenerateTsConfigCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter) { }

        public override int Run(GenerateTsConfigJsonOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);

            _fileWriter.WriteAllLines($"{directory}\\tsconfig.json", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Json, "Angular2TsConfig", BluePrintType.Angular2), name));

            try
            {
                _projectManager.Process(directory, "tsconfig.json", FileType.Json);
            }
            catch { }

            return exitCode;
        }
    }
}
