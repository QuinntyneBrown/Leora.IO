using static CommandLine.Parser;
using Leora.Services.Contracts;
using Leora.Models;

namespace Leora.Commands.CustomElements
{
    public abstract class BaseCommand<TOptions> where TOptions : new()
    {
        protected readonly ITemplateManager _templateManager;
        protected readonly ICustomElementsTemplateProcessor _templateProcessor;
        protected readonly INamingConventionConverter _namingConventionConverter;
        protected readonly IProjectManager _projectManager;
        protected readonly IFileWriter _fileWriter;

        public BaseCommand(ITemplateManager templateManager, ICustomElementsTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter)
        {
            _templateProcessor = templateProcessor;
            _templateManager = templateManager;
            _namingConventionConverter = namingConventionConverter;
            _projectManager = projectManager;
            _fileWriter = fileWriter;
        }

        public int Run(string[] args)
        {
            var options = new TOptions();
            Default.ParseArguments(args, options);
            return Run(options);
        }

        public string[] GetTemplate(FileType fileType, string templateName) => _templateManager.Get(fileType, templateName, BluePrintType.CustomElements);

        public abstract int Run(TOptions options);
    }
}
