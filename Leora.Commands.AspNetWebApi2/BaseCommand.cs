using static CommandLine.Parser;
using Leora.Services.Contracts;

namespace Leora.Commands.AspNetWebApi2
{
    public abstract class BaseCommand<TOptions> where TOptions : new()
    {
        protected readonly IDotNetTemplateProcessor _templateProcessor;
        protected readonly ITemplateManager _templateManager;

        public BaseCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor)
        {
            _templateProcessor = templateProcessor;
            _templateManager = templateManager;
        }

        public virtual int Run(string[] args)
        {
            var options = new TOptions();
            Default.ParseArguments(args, options);
            return Run(options);
        }

        public abstract int Run(TOptions options);
    }
}
