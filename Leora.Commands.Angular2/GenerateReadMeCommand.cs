using System;
using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Services.Contracts;
using Leora.Models;

namespace Leora.Commands.Angular2
{
    public class GenerateReadMeCommand: BaseCommand<GenerateReadMeOptions>, IGenerateReadMeCommand
    {
        public GenerateReadMeCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateReadMeOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            _fileWriter.WriteAllLines($"{directory}//README.md", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.MarkDown, "Angular2ReadMe", BluePrintType.Angular2), name));
            return exitCode;
        }
    }
}
