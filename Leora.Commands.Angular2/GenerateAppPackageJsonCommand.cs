using System;
using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Services.Contracts;
using Leora.Models;

namespace Leora.Commands.Angular2
{
    public class GenerateAppPackageJsonCommand : BaseCommand<GenerateAppPackageJsonOptions>, IGenerateAppPackageJsonCommand
    {
        public GenerateAppPackageJsonCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter) { }

        public override int Run(GenerateAppPackageJsonOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            _fileWriter.WriteAllLines($"{directory}//package.json", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Json, "Angular2AppPackageJson", BluePrintType.Angular2), name));

            try { _projectManager.Process(directory, $"package.json", FileType.Json); }
            catch { }

            return exitCode;
        }
    }
}
