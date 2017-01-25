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
        public GenerateQueryCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, INamespaceManager namespaceManager)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter, namespaceManager)
        {

        }

        public override int Run(GenerateQueryOptions options)
        {
            return Run(options.NameSpace, options.Directory, options.Name, options.RootNamespace);
        }

        public int Run(string namespacename, string directory, string name, string rootNamespace)
        {
            int exitCode = 1;
            var pascalCaseName = $"{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}Query.cs";
            var entityNamePascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            var templateCs = _templateManager.Get(FileType.CSharp, "CQRSQuery", "Queries", entityNamePascalCase, BluePrintType.CQRS);
            _fileWriter.WriteAllLines($"{directory}//{pascalCaseName}", _templateProcessor.ProcessTemplate(templateCs, name, namespacename, rootNamespace));
            _projectManager.Process(directory, $"{pascalCaseName}", FileType.CSharp);
            return exitCode;
        }
    }
}
