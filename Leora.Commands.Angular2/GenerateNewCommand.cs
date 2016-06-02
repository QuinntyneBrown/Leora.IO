using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.Angular2
{
    public class GenerateNewCommand : BaseCommand<GenerateNewOptions>, IGenerateNewCommand
    {
        public GenerateNewCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter)
            :base(templateManager,templateProcessor, namingConventionConverter,projectManager, fileWriter) { }

        public override int Run(GenerateNewOptions options) => Run(options.Name, options.Directory);        

        public int Run(string name, string directory)
        {
            int exitCode = 1;
            
			var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);

			var typeScriptFileName = $"{snakeCaseName}.component.ts";

            _fileWriter.WriteAllLines($"{baseFilePath}.component.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular1Component", BluePrintType.Angular1), name));

			_projectManager.Process(directory, typeScriptFileName, FileType.TypeScript);
            
            return exitCode;
        }

    }
}
