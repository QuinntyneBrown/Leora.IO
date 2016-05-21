using static CommandLine.Parser;
using Leora.Services.Contracts;

namespace Leora.Commands.Angular1
{
    public abstract class BaseCommand<TOptions> where TOptions: new()
    {
        protected readonly ITemplateManager _templateManager;
        protected readonly ITemplateProcessor _templateProcessor;
        protected readonly INamingConventionConverter _namingConventionConverter;

        public BaseCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter)
        {
            _templateProcessor = templateProcessor;
            _templateManager = templateManager;
            _namingConventionConverter = namingConventionConverter;
        }

        public BaseCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor)
            :this(templateManager,templateProcessor,null)
        {
 
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
