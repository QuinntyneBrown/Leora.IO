using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateActionCreatorCommand : BaseCommand<GenerateActionCreatorOptions>, IGenerateActionCreatorCommand
    {
        public GenerateActionCreatorCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter)
            :base(templateManager,templateProcessor, namingConventionConverter) { }

        public override int Run(GenerateActionCreatorOptions options) => Run(options.Directory, options.Name, options.Crud);

        public int Run(string directory, string name, bool crud)
        {
            int exitCode = 1;
            WriteAllLines($"{directory}//{name}.actions.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(Leora.Models.FileType.TypeScript, GetTemplateName(crud), "Angular1"), name));
            return exitCode;
        }

        public string GetTemplateName(bool crud)
        {
            return crud ? "Angular1ActionCreatorCrud" 
                : "Angular1ActionCreator";
        }
    }
}
