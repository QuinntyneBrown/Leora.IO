using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.Commands.Angular2
{
    public class GenerateModelCommand : BaseCommand<GenerateModelOptions>, IGenerateModelCommand
    {
        protected readonly IGenerateIndexFromFolderCommand _generateIndexFromFolderCommand;

        public GenerateModelCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter,
            IGenerateIndexFromFolderCommand generateIndexFromFolderCommand)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter)
        {
            _generateIndexFromFolderCommand = generateIndexFromFolderCommand;
        }

        public override int Run(GenerateModelOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var typeScriptFileName = $"{snakeCaseName}.model.ts";
            var baseFilePath = $"{directory}//{snakeCaseName}";

            _fileWriter.WriteAllLines($"{baseFilePath}.model.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2Model", BluePrintType.Angular2), name));

            _generateIndexFromFolderCommand.Run(name, directory);

            try
            {
                _projectManager.Process(directory, typeScriptFileName, FileType.TypeScript);

            }
            catch (Exception e)
            {

            }

            return exitCode;
        }
    }
}
