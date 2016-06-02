using Leora.Commands.Contracts;
using Leora.Commands.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;
using static System.IO.Directory;

namespace Leora.Commands
{
    public class GenerateCommandCommand : BaseCommand<GenerateCommandCommandOptions>, IGenerateCommandCommand
    {
        public GenerateCommandCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, INamespaceManager namespaceManager)
            :base(templateManager,templateProcessor, namingConventionConverter,projectManager,fileWriter, namespaceManager) { }

        public override int Run(GenerateCommandCommandOptions options) => Run(options.Name, options.Directory, options.Namespace);

        public int Run(string name, string directory, string namespacename)
        {
            var exitCode = 1;
            var pascalCaseName = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            var commandFileName = $"Generate{pascalCaseName}Command.cs";
            var commandInterfaceFileName = $"IGenerate{pascalCaseName}Command.cs";
            var commandOptionsFileName = $"Generate{pascalCaseName}Options.cs";

            CreateDirectory($"{directory}//Contracts");
            CreateDirectory($"{directory}//Options");
            
            _fileWriter.WriteAllLines($"{directory}//{commandFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "CliCommand", BluePrintType.Cli), name, namespacename));
            _fileWriter.WriteAllLines($"{directory}//Contracts//{commandInterfaceFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "CliCommandInterface", BluePrintType.Cli), name, namespacename));
            _fileWriter.WriteAllLines($"{directory}//Options//{commandOptionsFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "CliCommandOptions", BluePrintType.Cli), name, namespacename));
            _projectManager.Process(directory, commandFileName, FileType.CSharp);
            _projectManager.Process($"{directory}//Contracts", commandInterfaceFileName, FileType.CSharp);
            _projectManager.Process($"{directory}//Options", commandOptionsFileName, FileType.CSharp);
            return 1;
        }
    }
}
