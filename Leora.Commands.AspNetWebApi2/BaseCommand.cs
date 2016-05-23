using static CommandLine.Parser;
using Leora.Services.Contracts;

namespace Leora.Commands.AspNetWebApi2
{
    public abstract class BaseCommand<TOptions> where TOptions : new()
    {
        protected readonly ITemplateManager _templateManager;
        protected readonly IDotNetTemplateProcessor _templateProcessor;
        protected readonly INamingConventionConverter _namingConventionConverter;
        protected readonly IProjectManager _projectManager;

        public BaseCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager)
        {
            _templateProcessor = templateProcessor;
            _templateManager = templateManager;
            _namingConventionConverter = namingConventionConverter;
            _projectManager = projectManager;
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
