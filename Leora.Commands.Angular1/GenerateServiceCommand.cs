﻿using System;
using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateServiceCommand : BaseCommand<GenerateServiceOptions>, IGenerateServiceCommand
    {
        public GenerateServiceCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter)
            :base(templateManager,templateProcessor, namingConventionConverter,projectManager, fileWriter) { }

        public override int Run(GenerateServiceOptions options) => Run(options.Name, options.Directory, options.Crud, options.Data);

        public int Run(string name, string directory, bool crud, bool data)
        {
            int exitCode = 1;

            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var typeScriptFileName = $"{snakeCaseName}.service.ts";
            var filePath = $"{directory}//{typeScriptFileName}";

            if (crud)
                _fileWriter.WriteAllLines(filePath, _templateProcessor.ProcessTemplate(_templateManager.Get(Leora.Models.FileType.TypeScript, "Angular1DataServiceCrud", BluePrintType.Angular1), name));

            if (data)
                _fileWriter.WriteAllLines(filePath, _templateProcessor.ProcessTemplate(_templateManager.Get(Leora.Models.FileType.TypeScript, "Angular1DataService", BluePrintType.Angular1), name));

            if (!crud && !data)
                _fileWriter.WriteAllLines(filePath, _templateProcessor.ProcessTemplate(_templateManager.Get(Leora.Models.FileType.TypeScript, "Angular1Service","Angular1"), name));

            _projectManager.Process(directory, typeScriptFileName, FileType.TypeScript);

            return exitCode;
        }
    }
}
