using static CommandLine.Parser;
using Leora.Services.Contracts;
using System;

namespace Leora.Commands.AspNetWebApi2
{
    public abstract class BaseCommand<TOptions> where TOptions : Leora.Commands.AspNetWebApi2.Options.BaseOptions, new() 
    {
        protected readonly ITemplateManager _templateManager;
        protected readonly IDotNetTemplateProcessor _templateProcessor;
        protected readonly INamingConventionConverter _namingConventionConverter;
        protected readonly IProjectManager _projectManager;
        protected readonly INamespaceManager _namespaceManager;
        protected readonly IFileWriter _fileWriter;

        public BaseCommand(IFileWriter fileWriter, ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, INamespaceManager namespaceManager, IProjectManager projectManager)
        {
            _fileWriter = fileWriter;
            _templateProcessor = templateProcessor;
            _templateManager = templateManager;
            _namingConventionConverter = namingConventionConverter;
            _projectManager = projectManager;
            _namespaceManager = namespaceManager;
        }

        public virtual int Run(string[] args)
        {
            var options = new TOptions();
            Default.ParseArguments(args, options);
            options.NameSpace = _namespaceManager.GetNamespace(options.Directory).Namespace;
            options.RootNamespace = _namespaceManager.GetNamespace(options.Directory).RootNamespace;
            return Run(options);
        }

        public abstract int Run(TOptions options);
    }
}
