using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Services.Contracts;
using Leora.Models;
namespace Leora.Commands.Angular2
{
    public class GenerateNormalizeCommand: BaseCommand<GenerateNormalizeOptions>, IGenerateNormalizeCommand
    {
        public GenerateNormalizeCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateNormalizeOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            _fileWriter.WriteAllLines($"{directory}//_normalize.scss", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Scss, "Angular2Normalize", BluePrintType.Angular2), name));
            return exitCode;
        }
    }
}
