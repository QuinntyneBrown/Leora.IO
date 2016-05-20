using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Services.Contracts;
using static CommandLine.Parser;

namespace Leora.Commands.Angular2
{
    public class GenerateComponentCommand : IGenerateComponentCommand
    {
        private readonly ITemplateManager _templateManager;

        public GenerateComponentCommand(ITemplateManager templateManager)
        {
            _templateManager = templateManager;
        }

        public int Run(string[] args)
        {
            var options = new GenerateComponentOptions();
            Default.ParseArguments(args, options);

            return 1;
        }
    }
}
