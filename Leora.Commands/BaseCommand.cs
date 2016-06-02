using static CommandLine.Parser;
using Leora.Services.Contracts;
using Leora.Models;
using System;

namespace Leora.Commands
{
    public abstract class BaseCommand<TOptions> where TOptions : Leora.Commands.Options.BaseOptions, new()
    {
        protected readonly ITemplateManager _templateManager;
        protected readonly INamespaceManager _namespaceManager;
        protected readonly IDotNetTemplateProcessor _templateProcessor;
        protected readonly INamingConventionConverter _namingConventionConverter;
        protected readonly IProjectManager _projectManager;
        protected readonly IFileWriter _fileWriter;

        public BaseCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, INamespaceManager namespaceManager)
        {
            _templateProcessor = templateProcessor;
            _templateManager = templateManager;
            _namingConventionConverter = namingConventionConverter;
            _projectManager = projectManager;
            _fileWriter = fileWriter;
            _namespaceManager = namespaceManager;
        }

        public BaseCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter)
            : this(templateManager, templateProcessor, namingConventionConverter, null, null, null)
        {

        }

        public BaseCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor)
            : this(templateManager, templateProcessor, null, null, null, null)
        {

        }

        public int Run(string[] args)
        {
            var options = new TOptions();
            Default.ParseArguments(args, options);
            options.Namespace = _namespaceManager.GetNamespace(options.Directory).Namespace;            
            return Run(options);
        }

        public string[] GetTemplate(FileType fileType, string templateName)
        {
            return _templateManager.Get(fileType, templateName, BluePrintType.Cli);
        }

        public abstract int Run(TOptions options);
    }
}
