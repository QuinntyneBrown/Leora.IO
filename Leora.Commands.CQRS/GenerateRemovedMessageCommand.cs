using Leora.Commands.Contracts;
using Leora.Commands.CQRS.Core;
using Leora.Models;
using Leora.Services.Contracts;
using System;

namespace Leora.Commands.CQRS
{
    public class GenerateRemovedOptions : BaseOptions
    {
        public GenerateRemovedOptions()
            : base()
        {

        }
    }

    public interface IGenerateRemovedMessageCommand : ICommand
    {

    }

    public class GenerateRemovedMessageCommand : Leora.Commands.CQRS.Core.BaseCommand<GenerateRemovedOptions>, IGenerateRemovedMessageCommand
    {
        public GenerateRemovedMessageCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, INamespaceManager namespaceManager)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter, namespaceManager)
        {
        }

        public override int Run(GenerateRemovedOptions options)
        {
            return Run(options.Entity, options.Directory, options.NameSpace, options.RootNamespace);
        }

        public int Run(string entityName, string directory, string namespacename, string rootNamespace)
        {
            int exitCode = 1;
            var templateCs = _templateManager.Get(FileType.CSharp, "CQRSRemovedMessage", "Commands", _namingConventionConverter.Convert(NamingConvention.PascalCase, entityName), BluePrintType.CQRS);
            var entityNamePascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, entityName);
            _fileWriter.WriteAllLines($"{directory}//Removed{entityNamePascalCase}Message.cs", _templateProcessor.ProcessTemplate(templateCs, entityName, entityName, namespacename, rootNamespace));
            _projectManager.Process(directory, $"Removed{_namingConventionConverter.Convert(NamingConvention.PascalCase, entityName)}Message.cs", FileType.CSharp);
            return exitCode;
        }

    }
}
