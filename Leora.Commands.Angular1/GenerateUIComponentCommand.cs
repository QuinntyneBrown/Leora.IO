using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateUIComponentCommand : BaseCommand<GenerateUIComponentOptions>, IGenerateComponentCommand
    {
        public GenerateUIComponentCommand(ITemplateProcessor templateProcessor, ITemplateManager templateManager)
            : base(templateManager, templateProcessor) { }

        public override int Run(GenerateUIComponentOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            int exitCode = 1;
            WriteAllLines($"{directory}/{name}.component.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular1UIComponent"), name));
            WriteAllLines($"{directory}/{name}.component.css", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Css, "Angular1UIComponent"), name));
            WriteAllLines($"{directory}/{name}.component.html", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Html, "Angular1UIComponent"), name));
            return exitCode;
        }
    }
}
