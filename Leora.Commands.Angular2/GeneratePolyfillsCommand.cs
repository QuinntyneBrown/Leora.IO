using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Services.Contracts;
using Leora.Models;s;

namespace Leora.Commands.Angular2
{
    public class GeneratePolyfillsCommand: BaseCommand<GeneratePolyfillsOptions>, IGeneratePolyfillsCommand
    {
        public GeneratePolyfillsCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GeneratePolyfillsOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            _fileWriter.WriteAllLines($"{directory}//polyfills.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2Polyfills", BluePrintType.Angular2), name));
            return exitCode;
        }
    }
}
