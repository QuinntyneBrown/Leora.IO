using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Models;
using Leora.Services.Contracts;

namespace Leora.Commands.Angular2
{
    public class GenerateRxJSExtensionsCommand: BaseCommand<GenerateRxJSExtensionsOptions>, IGenerateRxJSExtensionsCommand
    {
        protected readonly IGenerateIndexFromFolderCommand _generateIndexFromFolderCommand;

        public GenerateRxJSExtensionsCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter,
            IGenerateIndexFromFolderCommand generateIndexFromFolderCommand)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter)
        {
            _generateIndexFromFolderCommand = generateIndexFromFolderCommand;
        }

        public override int Run(GenerateRxJSExtensionsOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var typeScriptFileName = $"rxjs-extensions.ts";            
            _fileWriter.WriteAllLines($"{directory}//{typeScriptFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2RxExtensions", BluePrintType.Angular2), name));

            _generateIndexFromFolderCommand.Run(name, directory);

            try
            {
                _projectManager.Process(directory, typeScriptFileName, FileType.TypeScript);
            }
            catch { }

            return exitCode;
        }
    }
}
