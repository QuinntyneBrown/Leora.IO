﻿using Leora.Models;
using Leora.Services.Contracts;
using static CommandLine.Parser;

namespace Leora.Commands.CQRS.Core
{
    public abstract class BaseCommand<TOptions> where TOptions : Leora.Commands.CQRS.Core.BaseOptions, new()
    {
        protected readonly ITemplateManager _templateManager;
        protected readonly IDotNetTemplateProcessor _templateProcessor;
        protected readonly INamingConventionConverter _namingConventionConverter;
        protected readonly IProjectManager _projectManager;
        protected readonly IFileWriter _fileWriter;
        protected readonly INamespaceManager _namespaceManager;
        protected readonly ILeoraJSONFileManager _leoraJSONFileManager;
        public BaseCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, INamespaceManager namespaceManager, ILeoraJSONFileManager leoraJSONFileManager)
        {
            _templateProcessor = templateProcessor;
            _templateManager = templateManager;
            _namingConventionConverter = namingConventionConverter;
            _projectManager = projectManager;
            _fileWriter = fileWriter;
            _namespaceManager = namespaceManager;
            _leoraJSONFileManager = leoraJSONFileManager;
        }

        public virtual int Run(string[] args)
        {
            var options = new TOptions();
            Default.ParseArguments(args, options);
            options.NameSpace = _namespaceManager.GetNamespace(options.Directory).Namespace;
            options.RootNamespace = _namespaceManager.GetNamespace(options.Directory).RootNamespace;
            options.Framework = _leoraJSONFileManager.GetLeoraJSONFile(options.Directory, -1).Framework;
            return Run(options);
        }

        public string[] GetTemplate(FileType fileType, string templateName) => _templateManager.Get(fileType, templateName, BluePrintType.CQRS);

        public abstract int Run(TOptions options);

        public bool IsVanillaSql(string directory) {
            return _leoraJSONFileManager.GetLeoraJSONFile(directory, -1).VanillaSql;
        }
    }
}
