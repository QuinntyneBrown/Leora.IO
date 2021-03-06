﻿using Leora.Commands.Contracts;
using Leora.Commands.CQRS.Core;
using Leora.Models;
using Leora.Services.Contracts;
using System;

namespace Leora.Commands.CQRS
{
    public class GenerateRemoveOptions: BaseOptions
    {

    }

    public interface IGenerateRemoveCommand: ICommand
    {

    }

    public class GenerateRemoveCommand : Leora.Commands.CQRS.Core.BaseCommand<GenerateRemoveOptions>, IGenerateRemoveCommand
    {
        public GenerateRemoveCommand(ILeoraJSONFileManager leoraJSONFileManager, ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, INamespaceManager namespaceManager)
            :base(templateManager,templateProcessor, namingConventionConverter, projectManager,fileWriter, namespaceManager, leoraJSONFileManager)
        {

        }

        public override int Run(GenerateRemoveOptions options)
        {
            if (string.IsNullOrEmpty(options.Name))
            {
                options.Name = $"Remove{options.Entity}";
            }

            return Run(options.Entity, options.NameSpace, options.Directory, options.Name, options.RootNamespace, options.Framework);
        }

        public int Run(string entityName, string namespacename, string directory, string name, string rootNamespace, string framework)
        {
            int exitCode = 1;

            var templateCs = _templateManager.Get(FileType.CSharp, "CQRSRemove", "Commands", _namingConventionConverter.Convert(NamingConvention.PascalCase, name), framework);
            _fileWriter.WriteAllLines($"{directory}//{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}Command.cs", _templateProcessor.ProcessTemplate(templateCs, entityName, name, namespacename, rootNamespace));
            _projectManager.Process(directory, $"{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}Command.cs", FileType.CSharp);
            return exitCode;
        }
    }
}
