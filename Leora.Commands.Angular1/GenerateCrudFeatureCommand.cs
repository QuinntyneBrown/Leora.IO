using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateCrudFeatureCommand : BaseCommand<GenerateCrudFeatureOptions>, IGenerateComponentCommand
    {
        public GenerateCrudFeatureCommand(ITemplateProcessor templateProcessor, ITemplateManager templateManager)
            :base(templateManager,templateProcessor)
        {

        }

        public override int Run(GenerateCrudFeatureOptions options)
        {
            return Run(options.Name, options.Directory);
        }

        public int Run(string name, string directory)
        {
            throw new NotImplementedException();
        }
    }
}
