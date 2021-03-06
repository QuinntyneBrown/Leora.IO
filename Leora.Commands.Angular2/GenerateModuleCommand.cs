﻿using System;
using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Services.Contracts;
using Leora.Models;

namespace Leora.Commands.Angular2
{
    public class GenerateModuleCommand: BaseCommand<GenerateModuleOptions>, IGenerateModuleCommand
    {
        public GenerateModuleCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateModuleOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var entityNameSnakeCase = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var entityNamePascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            var template = _templateManager.Get(FileType.TypeScript, "Angular2Module", "Modules", entityNamePascalCase, BluePrintType.Angular2);

            _fileWriter.WriteAllLines($"{directory}//{entityNameSnakeCase}s.module.ts", _templateProcessor.ProcessTemplate(template, name));

            try { _projectManager.Process(directory, $"{entityNameSnakeCase}s.module.ts", FileType.TypeScript); }
            catch { }

            return exitCode;
        }
    }
}
