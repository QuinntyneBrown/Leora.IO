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
    public class GenerateUtilitiesCommand: BaseCommand<GenerateUtilitiesOptions>, IGenerateUtilitiesCommand
    {
        protected readonly IGenerateIndexFromFolderCommand _generateIndexFromFolderCommand;

        public GenerateUtilitiesCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, GenerateIndexFromFolderCommand generateIndexFromFolderCommand) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) {
            _generateIndexFromFolderCommand = generateIndexFromFolderCommand;
        }

        public override int Run(GenerateUtilitiesOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var entityNameSnakeCase = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);

            
            _fileWriter.WriteAllLines($"{directory}//add-or-update.ts",
                _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2AddOrUpdate", BluePrintType.Angular2), name));

            _fileWriter.WriteAllLines($"{directory}//append-to-body-async.ts",
                _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2AppendToBodyAsync", BluePrintType.Angular2), name));

            _fileWriter.WriteAllLines($"{directory}//create-element.ts",
                _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2CreateElement", BluePrintType.Angular2), name));

            _fileWriter.WriteAllLines($"{directory}//extend-css-async.ts",
                _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2ExtendCssAsync", BluePrintType.Angular2), name));

            
            _fileWriter.WriteAllLines($"{directory}//extract-data-from-http-response.ts",
                _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2ExtractDataFromHttpResponse", BluePrintType.Angular2), name));

            _fileWriter.WriteAllLines($"{directory}//form-encode.ts",
                _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2FormEncode", BluePrintType.Angular2), name));
            

            _fileWriter.WriteAllLines($"{directory}//guid.ts",
                _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2Guid", BluePrintType.Angular2), name));

            _fileWriter.WriteAllLines($"{directory}//pluck-out.ts",
                _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2PluckOut", BluePrintType.Angular2), name));

            _fileWriter.WriteAllLines($"{directory}//pluck.ts",
                _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2Pluck", BluePrintType.Angular2), name));

            _fileWriter.WriteAllLines($"{directory}//remove-element.ts",
                _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2RemoveElement", BluePrintType.Angular2), name));

            _fileWriter.WriteAllLines($"{directory}//set-opacity-async.ts",
                _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2SetOpacityAsync", BluePrintType.Angular2), name));

            _generateIndexFromFolderCommand.Run(name, directory);

            try
            {
                _projectManager.Process(directory, "add-or-update.ts", FileType.TypeScript);
                _projectManager.Process(directory, "append-to-body-async.ts", FileType.TypeScript);
                _projectManager.Process(directory, "create-element.ts", FileType.TypeScript);                
                _projectManager.Process(directory, "extend-css-async.ts", FileType.TypeScript);
                _projectManager.Process(directory, "extract-data-from-http-response.ts", FileType.TypeScript);
                _projectManager.Process(directory, "form-encode.ts", FileType.TypeScript);
                _projectManager.Process(directory, "guid.ts", FileType.TypeScript);
                _projectManager.Process(directory, "pluck-out.ts", FileType.TypeScript);
                _projectManager.Process(directory, "pluck.ts", FileType.TypeScript);
                _projectManager.Process(directory, "remove-element.ts", FileType.TypeScript);
                _projectManager.Process(directory, "set-opacity-async.ts", FileType.TypeScript);

            }
            catch { }

            _generateIndexFromFolderCommand.Run(name, directory);

            return exitCode;
        }
    }
}
