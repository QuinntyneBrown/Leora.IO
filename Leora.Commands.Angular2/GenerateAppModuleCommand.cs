using System;
using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Services.Contracts;
using Leora.Models;

namespace Leora.Commands.Angular2
{
    public class GenerateAppModuleCommand: BaseCommand<GenerateAppModuleOptions>, IGenerateAppModuleCommand
    {
        public GenerateAppModuleCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateAppModuleOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var entityNameSnakeCase = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            _fileWriter.WriteAllLines($"{directory}//{entityNameSnakeCase}.module.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2AppModule", BluePrintType.Angular2), name));

            try { _projectManager.Process(directory, $"{entityNameSnakeCase}.module.ts", FileType.TypeScript); }
            catch { }

            return exitCode;
        }
    }
}
