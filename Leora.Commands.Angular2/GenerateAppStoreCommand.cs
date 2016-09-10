using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Models;
using Leora.Services.Contracts;

namespace Leora.Commands.Angular2
{
    public class GenerateAppStoreCommand: BaseCommand<GenerateAppStoreOptions>, IGenerateAppStoreCommand
    {
        public GenerateAppStoreCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateAppStoreOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;

            _fileWriter.WriteAllLines($"{directory}//app-store.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2AppStore", BluePrintType.Angular2), name));

            try { _projectManager.Process(directory, "app-store.ts", FileType.TypeScript); }
            catch { }

            return exitCode;
        }
    }
}
