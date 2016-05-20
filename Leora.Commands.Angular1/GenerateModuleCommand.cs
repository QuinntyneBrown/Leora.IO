using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateModuleCommand : BaseCommand<GenerateModuleOptions>, IGenerateModuleCommand
    {
        public GenerateModuleCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor)
            : base(templateManager, templateProcessor)
        { }

        public override int Run(GenerateModuleOptions options) => Run(options.Directory, options.Name);

        public int Run(string directory, string name)
        {
            int exitCode = 1;
            WriteAllLines($"{directory}/{name}.component.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, _componentName), name));
            WriteAllLines($"{directory}/{name}.component.css", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Css, _componentName), name));
            WriteAllLines($"{directory}/{name}.component.html", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Html, _componentName), name));
            return exitCode;
        }

        private readonly string _componentName = "Angular1ModuleComponent";
    }
}
