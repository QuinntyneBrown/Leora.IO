using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Services.Contracts;
using Leora.Models;

namespace Leora.Commands.Angular2
{
    public class GenerateRoutingCommand: BaseCommand<GenerateRoutingOptions>, IGenerateRoutingCommand
    {
        public GenerateRoutingCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateRoutingOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var entityNameSnakeCase = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            _fileWriter.WriteAllLines($"{directory}//{entityNameSnakeCase}.routing.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2Routing", BluePrintType.Angular2), name));

            try { _projectManager.Process(directory, $"{entityNameSnakeCase}.routing.ts", FileType.TypeScript); }
            catch { }

            return exitCode;
        }
    }
}
