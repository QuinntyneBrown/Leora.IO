﻿using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateDtosCommand : BaseCommand<GenerateDtosOptions>, IGenerateDtosCommand
    {
        public GenerateDtosCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager) { }

        public override int Run(GenerateDtosOptions options) => Run(options.NameSpace, options.Directory, options.Name);

        public int Run(string namespacename, string directory, string name)
        {
            int exitCode = 1;
            var pascalCaseName = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            var dtoName = $"{pascalCaseName}Dto";
            var addOrUpdateRequestName = $"{pascalCaseName}AddOrUpdateRequestDto.cs";
            var addOrUpdateResponseName = $"{pascalCaseName}AddOrUpdateResponseDto.cs";

            WriteAllLines($"{directory}//{dtoName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, BluePrintType.AspNetWebApi2), name, namespacename));
            WriteAllLines($"{directory}//{addOrUpdateRequestName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, BluePrintType.AspNetWebApi2), name, namespacename));
            WriteAllLines($"{directory}//{addOrUpdateResponseName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, BluePrintType.AspNetWebApi2), name, namespacename));

            _projectManager.Add(directory, $"{dtoName}", FileType.CSharp);
            _projectManager.Add(directory, $"{addOrUpdateRequestName}", FileType.CSharp);
            _projectManager.Add(directory, $"{addOrUpdateResponseName}", FileType.CSharp);

            return exitCode;
        }
    }
}
