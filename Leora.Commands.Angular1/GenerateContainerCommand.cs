using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateContainerCommand : BaseCommand<GenerateContainerOptions>, IGenerateContainerCommand
    {
        public GenerateContainerCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor)
            : base(templateManager, templateProcessor) { }

        public override int Run(GenerateContainerOptions options)
        {
            return Run(options.Name, options.Directory);
        }

        public int Run(string name, string directory)
        {
            int exitCode = 1;
            WriteAllLines($"{directory}/{name}.container.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular1Container"), name));
            WriteAllLines($"{directory}/{name}.container.css", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Css, "Angular1Container"), name));
            WriteAllLines($"{directory}/{name}.container.html", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Html, "Angular1Container"), name));
            return exitCode;
        }

    }
}
