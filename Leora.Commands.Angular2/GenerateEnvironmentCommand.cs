using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Models;
using Leora.Services.Contracts;

namespace Leora.Commands.Angular2
{
    public class GenerateEnvironmentCommand: BaseCommand<GenerateEnvironmentOptions>, IGenerateEnvironmentCommand
    {
        public GenerateEnvironmentCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateEnvironmentOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var entityNameSnakeCase = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);

            _fileWriter.WriteAllLines($"{directory}//environment.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2Actions", BluePrintType.Angular2), name));

            try
            {
                _projectManager.Process(directory, "environment.ts", FileType.TypeScript);
            }
            catch
            {

            }

            return exitCode;
        }
    }
}
