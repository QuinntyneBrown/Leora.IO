﻿using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateModuleCommand : BaseCommand<GenerateModuleOptions>, IGenerateModuleCommand
    {
        public GenerateModuleCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager) { }

        public override int Run(GenerateModuleOptions options) => Run(options.Name, options.Directory, options.Crud);

        public int Run(string name, string directory, bool crud)
        {
            int exitCode = 1;

            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var typeScriptFileName = $"{ snakeCaseName }.module.ts";

            WriteAllLines($"{directory}//{ typeScriptFileName }", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, GetTemplateName(crud), "Angular1"), name));

            _projectManager.Add(directory, typeScriptFileName, FileType.TypeScript);

            return exitCode;
        }

        public string GetTemplateName(bool crud)
        {
            return crud ? "Angular1ModuleCrud"
                : "Angular1Module";
        }
    }
}
