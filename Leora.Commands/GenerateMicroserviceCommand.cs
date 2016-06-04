using Leora.Commands.Contracts;
using Leora.Commands.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;
using static CommandLine.Parser;
using static System.IO.Directory;

namespace Leora.Commands
{
    public class GenerateMicroserviceCommand :  IGenerateMicroserviceCommand
    {
        protected readonly ITemplateManager _templateManager;
        protected readonly INamespaceManager _namespaceManager;
        protected readonly IMicroserviceTemplateProcessor _templateProcessor;
        protected readonly INamingConventionConverter _namingConventionConverter;
        protected readonly IProjectManager _projectManager;
        protected readonly IFileWriter _fileWriter;

        public GenerateMicroserviceCommand(ITemplateManager templateManager, IMicroserviceTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, INamespaceManager namespaceManager)
        {
            _templateManager = templateManager;
            _templateProcessor = templateProcessor;
            _namespaceManager = namespaceManager;
            _namingConventionConverter = namingConventionConverter;
            _projectManager = projectManager;
            _fileWriter = fileWriter;
        }

        public int Run(string[] args)
        {
            var options = new GenerateMicroserviceOptions();
            Default.ParseArguments(args, options);
            return Run(options);
        }

        public int Run(GenerateMicroserviceOptions options) => Run(options.Name, options.Directory, options.Entity);        

        public int Run(string name, string directory, string entity)
        {
            int exitCode = 1;

            string namePascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            string entityPascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, entity);

            string microserviceDirectoryName = $"{directory}//{namePascalCase}";
            CreateDirectory(microserviceDirectoryName);
            _fileWriter.WriteAllLines($"{microserviceDirectoryName}//{namePascalCase}.sln", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroService.sln", BluePrintType.MicroService), name, entity));


            string apiDirectoryName = $"{microserviceDirectoryName}//{namePascalCase}";
            CreateDirectory(apiDirectoryName);

            string cliDirectoryName = $"{microserviceDirectoryName}//{namePascalCase}.Cli";
            CreateDirectory(cliDirectoryName);

            string webDirectoryName = $"{microserviceDirectoryName}//{namePascalCase}.Web";
            CreateDirectory(webDirectoryName);

            string testsDirectoryName = $"{microserviceDirectoryName}//{namePascalCase}.Tests";
            CreateDirectory(testsDirectoryName);

            //var pascalCaseName = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            //var commandFileName = $"Generate{pascalCaseName}Command.cs";
            //var commandInterfaceFileName = $"IGenerate{pascalCaseName}Command.cs";
            //var commandOptionsFileName = $"Generate{pascalCaseName}Options.cs";

            //CreateDirectory($"{directory}//Contracts");
            //CreateDirectory($"{directory}//Options");

            //_fileWriter.WriteAllLines($"{directory}//{commandFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "CliCommand", BluePrintType.Cli), name, namespacename));
            //_fileWriter.WriteAllLines($"{directory}//Contracts//{commandInterfaceFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "CliCommandInterface", BluePrintType.Cli), name, namespacename));
            //_fileWriter.WriteAllLines($"{directory}//Options//{commandOptionsFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "CliCommandOptions", BluePrintType.Cli), name, namespacename));
            //_projectManager.Process(directory, commandFileName, FileType.CSharp);
            //_projectManager.Process($"{directory}//Contracts", commandInterfaceFileName, FileType.CSharp);
            //_projectManager.Process($"{directory}//Options", commandOptionsFileName, FileType.CSharp);

            return exitCode;
        }

    }
}
