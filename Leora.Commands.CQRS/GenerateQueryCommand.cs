using Leora.Commands.Contracts;
using Leora.Commands.CQRS.Core;
using Leora.Models;
using Leora.Services.Contracts;
using System;

namespace Leora.Commands.CQRS
{
    public class GenerateQueryOptions : BaseOptions
    {

    }

    public interface IGenerateQueryCommand: ICommand
    {

    }

    public class GenerateQueryCommand : Leora.Commands.CQRS.Core.BaseCommand<GenerateQueryOptions>, IGenerateQueryCommand
    {
        public GenerateQueryCommand(ILeoraJSONFileManager leoraJSONFileManager, ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, INamespaceManager namespaceManager)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter, namespaceManager, leoraJSONFileManager)
        {

        }

        public override int Run(GenerateQueryOptions options)
        {
            return Run(options.Entity, options.NameSpace, options.Directory, options.Name, options.RootNamespace, options.Framework);
        }

        public int Run(string entityName, string namespacename, string directory, string name, string rootNamespace, string framework)
        {
            int exitCode = 1;

            var templateCs = _templateManager.Get(FileType.CSharp, "CQRSQuery", "Queries", _namingConventionConverter.Convert(NamingConvention.PascalCase, name), framework);
            _fileWriter.WriteAllLines($"{directory}//{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}Query.cs", _templateProcessor.ProcessTemplate(templateCs, entityName, name, namespacename, rootNamespace));
            _projectManager.Process(directory, $"{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}Query.cs", FileType.CSharp);
            return exitCode;
        }
    }
}
