using Leora.Commands.Contracts;
using Leora.Commands.CQRS.Core;
using Leora.Models;
using Leora.Services.Contracts;
using System;

namespace Leora.Commands.CQRS
{
    public class GenerateEventBusMessagesOptions : BaseOptions
    {
        public GenerateEventBusMessagesOptions()
            : base()
        {

        }
    }

    public interface IGenerateEventBusMessagesCommand : ICommand { }

    public class GenerateEventBusMessagesCommand : Leora.Commands.CQRS.Core.BaseCommand<GenerateEventBusMessagesOptions>, IGenerateEventBusMessagesCommand
    {
        public GenerateEventBusMessagesCommand(ILeoraJSONFileManager leoraJSONFileManager, ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, INamespaceManager namespaceManager)
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter,namespaceManager, leoraJSONFileManager) {
        }
        
        public override int Run(GenerateEventBusMessagesOptions options)
        {
            return Run(options.Entity, options.Directory, options.NameSpace, options.RootNamespace);
        }

        public int Run(string entityName, string directory, string namespacename, string rootNamespace)
        {
            int exitCode = 1;            
            var templateCs = _templateManager.Get(FileType.CSharp, "CQRSEventBusMessages", "Commands", _namingConventionConverter.Convert(NamingConvention.PascalCase, entityName), BluePrintType.CQRS);
            var entityNamePascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, entityName);
            _fileWriter.WriteAllLines($"{directory}//{entityNamePascalCase}sEventBusMessages.cs", _templateProcessor.ProcessTemplate(templateCs, entityName, entityName, namespacename, rootNamespace));
            _projectManager.Process(directory, $"{_namingConventionConverter.Convert(NamingConvention.PascalCase, entityName)}sEventBusMessages.cs", FileType.CSharp);
            return exitCode;
        }
    }
}
