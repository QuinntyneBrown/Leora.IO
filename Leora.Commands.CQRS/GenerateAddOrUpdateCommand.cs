using Leora.Commands.Contracts;
using Leora.Commands.CQRS.Core;
using Leora.Models;
using Leora.Services.Contracts;
using System;

namespace Leora.Commands.CQRS
{
    public class GenerateAddOrUpdateOptions: BaseOptions
    {
        public GenerateAddOrUpdateOptions()
            :base()
        {

        }
    }

    public interface IGenerateAddOrUpdateCommand: ICommand
    {

    }

    public class GenerateAddOrUpdateCommand : Leora.Commands.CQRS.Core.BaseCommand<GenerateAddOrUpdateOptions>, IGenerateAddOrUpdateCommand
    {
        public GenerateAddOrUpdateCommand(ITemplateManager templateManager, 
            IDotNetTemplateProcessor templateProcessor, 
            INamingConventionConverter namingConventionConverter, 
            IProjectManager projectManager, IFileWriter fileWriter, 
            INamespaceManager namespaceManager,
            ILeoraJSONFileManager leoraJSONFileManager)
            :base(templateManager,templateProcessor, namingConventionConverter, projectManager,fileWriter, namespaceManager)
        {
            _leoraJSONFileManager = leoraJSONFileManager;
        }

        public override int Run(GenerateAddOrUpdateOptions options)
        {
            var s = _leoraJSONFileManager.GetLeoraJSONFile(options.Directory, -1);
            Console.WriteLine(s.UseMessaging);

            if (string.IsNullOrEmpty(options.Name))
            {
                options.Name = $"AddOrUpdate{options.Entity}";
            }

            return Run(options.Entity, options.NameSpace, options.Directory, options.Name, options.RootNamespace);
        }

        public int Run(string entityName, string namespacename, string directory, string name, string rootNamespace)
        {
            int exitCode = 1;

            var templateCs = _templateManager.Get(FileType.CSharp, "CQRSAddOrUpdate", "Commands", _namingConventionConverter.Convert(NamingConvention.PascalCase, name), BluePrintType.CQRS);
            _fileWriter.WriteAllLines($"{directory}//{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}Command.cs", _templateProcessor.ProcessTemplate(templateCs, entityName, name, namespacename, rootNamespace));
            _projectManager.Process(directory, $"{_namingConventionConverter.Convert(NamingConvention.PascalCase, name)}Command.cs", FileType.CSharp);
            return exitCode;
        }

        protected ILeoraJSONFileManager _leoraJSONFileManager;
    }
}
