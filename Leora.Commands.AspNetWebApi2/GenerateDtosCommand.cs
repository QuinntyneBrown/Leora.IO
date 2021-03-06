﻿using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateDtosCommand : BaseCommand<GenerateDtosOptions>, IGenerateDtosCommand
    {
        public GenerateDtosCommand(IFileWriter fileWriter,
            ITemplateManager templateManager, 
            IDotNetTemplateProcessor templateProcessor, 
            INamingConventionConverter namingConventionConverter, 
            INamespaceManager namespaceManager,
            IProjectManager projectManager,
            ILeoraJSONFileManager leoraJSONFileManager)
            : base(fileWriter, templateManager, templateProcessor, namingConventionConverter, namespaceManager, projectManager,leoraJSONFileManager) { }

        public override int Run(GenerateDtosOptions options) => Run(options.NameSpace, options.Directory, options.Name, options.RootNamespace, options.Framework);

        public int Run(string namespacename, string directory, string name, string rootNamespace, string framework)
        {
            int exitCode = 1;
            var pascalCaseName = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            var dtoName = $"{pascalCaseName}Dto.cs";
            var addOrUpdateRequestName = $"{pascalCaseName}AddOrUpdateRequestDto.cs";
            var addOrUpdateResponseName = $"{pascalCaseName}AddOrUpdateResponseDto.cs";

            _fileWriter.WriteAllLines($"{directory}//{dtoName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "ApiDto", BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));
            _fileWriter.WriteAllLines($"{directory}//{addOrUpdateRequestName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "ApiRequestDto", BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));
            _fileWriter.WriteAllLines($"{directory}//{addOrUpdateResponseName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "ApiResponseDto", BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));

            _projectManager.Process(directory, $"{dtoName}", FileType.CSharp);
            _projectManager.Process(directory, $"{addOrUpdateRequestName}", FileType.CSharp);
            _projectManager.Process(directory, $"{addOrUpdateResponseName}", FileType.CSharp);

            return exitCode;
        }
    }
}
