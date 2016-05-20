using System;
using Leora.Commands.Contracts;
using static CommandLine.Parser;
using Leora.Services.Contracts;

namespace Leora.Commands.Angular1
{
    public abstract class BaseCommand<TOptions> where TOptions: new()
    {
        protected readonly ITemplateManager _templateManager;
        protected readonly ITemplateProcessor _templateProcessor;

        public BaseCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor)
        {
            _templateProcessor = templateProcessor;
            _templateManager = templateManager;
        }

        public int Run(string[] args)
        {
            var options = new TOptions();
            Default.ParseArguments(args, options);
            return Run(options);
        }

        public abstract int Run(TOptions options);
    }
}
