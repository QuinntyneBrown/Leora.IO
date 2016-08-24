using System;
using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Services.Contracts;
using Leora.Models;

namespace Leora.Commands.Angular2
{
    public class GenerateReducerCommand: BaseCommand<GenerateReducerOptions>, IGenerateReducerCommand
    {
        public GenerateReducerCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateReducerOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var entityNameSnakeCase = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            _fileWriter.WriteAllLines($"{directory}//{entityNameSnakeCase}.reducer.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2Reducer", BluePrintType.Angular2), name));

            try { _projectManager.Process(directory, $"{entityNameSnakeCase}.reducer.ts", FileType.TypeScript); }
            catch { }

            return exitCode;
        }
    }
}
