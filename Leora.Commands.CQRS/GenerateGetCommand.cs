using Leora.Commands.Contracts;
using Leora.Commands.CQRS.Core;
using Leora.Models;
using Leora.Services.Contracts;
using System;

namespace Leora.Commands.CQRS
{
    public class GenerateGetOptions : BaseOptions
    {

    }

    public interface IGenerateGetCommand: ICommand
    {

    }

    public class GenerateGetCommand : Leora.Commands.CQRS.Core.BaseCommand<GenerateGetOptions>, IGenerateGetCommand
    {
        public GenerateGetCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, INamespaceManager namespaceManager)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter, namespaceManager)
        {

        }

        public override int Run(GenerateGetOptions options)
        {
            if (string.IsNullOrEmpty(options.Name))
            {
                options.Name = $"Get{options.Entity}s";
            }

            return Run(options.Entity, options.NameSpace, options.Directory, options.Name, options.RootNamespace);
        }

        public int Run(string entityName, string namespacename, string directory, string name, string rootNamespace)
        {
            int exitCode = 1;

            var templateCs = _templateManager.Get(FileType.CSharp, "CQRSGet", "Queries", _namingConventionConverter.Convert(NamingConvention.PascalCase, name), BluePrintType.CQRS);
            _fileWriter.WriteAllLines($"{directory}//{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}Query.cs", _templateProcessor.ProcessTemplate(templateCs, entityName, name, namespacename, rootNamespace));
            _projectManager.Process(directory, $"{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}Query.cs", FileType.CSharp);
            return exitCode;
        }
    }
}
