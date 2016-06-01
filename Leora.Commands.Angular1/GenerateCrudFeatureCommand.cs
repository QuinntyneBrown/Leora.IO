using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Services.Contracts;

namespace Leora.Commands.Angular1
{
    public class GenerateCrudFeatureCommand : BaseCommand<GenerateCrudFeatureOptions>, IGenerateCrudFeatureCommand
    {
        public GenerateCrudFeatureCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter)
            :base(templateManager,templateProcessor, namingConventionConverter,projectManager, fileWriter) { }

        public override int Run(GenerateCrudFeatureOptions options)
        {
            return Run(options.Name, options.Directory);
        }

        public int Run(string name, string directory)
        {
            return 1;
        }
    }
}
