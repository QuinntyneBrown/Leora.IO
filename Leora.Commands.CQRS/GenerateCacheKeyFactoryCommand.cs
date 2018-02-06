using Leora.Commands.Contracts;
using Leora.Commands.CQRS.Core;
using Leora.Models;
using Leora.Services.Contracts;
using System;

namespace Leora.Commands.CQRS
{
    public class GenerateCacheKeyFactoryOptions : BaseOptions
    {
        public GenerateCacheKeyFactoryOptions()
            : base()
        {

        }
    }

    public interface IGenerateCacheKeyFactoryCommand : ICommand { }

    public class GenerateCacheKeyFactoryCommand : Leora.Commands.CQRS.Core.BaseCommand<GenerateCacheKeyFactoryOptions>, IGenerateCacheKeyFactoryCommand
    {
        public GenerateCacheKeyFactoryCommand(ILeoraJSONFileManager leoraJSONFileManager, ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, INamespaceManager namespaceManager)
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter,namespaceManager, leoraJSONFileManager) {
        }
        
        public override int Run(GenerateCacheKeyFactoryOptions options)
        {
            return Run(options.Entity, options.Directory, options.NameSpace, options.RootNamespace);
        }

        public int Run(string entityName, string directory, string namespacename, string rootNamespace)
        {
            int exitCode = 1;            
            var templateCs = _templateManager.Get(FileType.CSharp, "CQRSCacheKeyFactory", "Commands", _namingConventionConverter.Convert(NamingConvention.PascalCase, entityName), BluePrintType.CQRS);
            var entityNamePascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, entityName);
            _fileWriter.WriteAllLines($"{directory}//{entityNamePascalCase}sCacheKeyFactory.cs", _templateProcessor.ProcessTemplate(templateCs, entityName, entityName, namespacename, rootNamespace));
            _projectManager.Process(directory, $"{_namingConventionConverter.Convert(NamingConvention.PascalCase, entityName)}sCacheKeyFactory.cs", FileType.CSharp);
            return exitCode;
        }
    }
}
