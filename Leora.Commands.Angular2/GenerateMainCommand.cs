using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Models;
using Leora.Services.Contracts;

namespace Leora.Commands.Angular2
{
    public class GenerateMainCommand: BaseCommand<GenerateMainOptions>, IGenerateMainCommand
    {
        public GenerateMainCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateMainOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            _fileWriter.WriteAllLines($"{directory}//main.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2Polyfills", BluePrintType.Angular2), name));

            try { _projectManager.Process(directory, $"main.ts", FileType.TypeScript); }
            catch { }

            return exitCode;
        }
    }
}
