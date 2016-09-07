using Leora.Commands.Contracts;
using Leora.Commands.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;
using static CommandLine.Parser;
using static System.IO.Directory;
using System.Collections.Generic;
using System;
using Leora.Commands.Angular2.Contracts;

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

        protected readonly IGenerateAppCommand _generateAppCommand;

        public GenerateMicroserviceCommand(
            ITemplateManager templateManager, 
            IMicroserviceTemplateProcessor templateProcessor, 
            INamingConventionConverter namingConventionConverter, 
            IProjectManager projectManager, 
            IFileWriter fileWriter, 
            INamespaceManager namespaceManager,
            
            IGenerateAppCommand generateAppCommand)
        {
            _templateManager = templateManager;
            _templateProcessor = templateProcessor;
            _namespaceManager = namespaceManager;
            _namingConventionConverter = namingConventionConverter;
            _projectManager = projectManager;
            _fileWriter = fileWriter;

            _generateAppCommand = generateAppCommand;
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

            CreateApi(microserviceDirectoryName, namePascalCase, entityPascalCase);
            CreateWeb(microserviceDirectoryName, namePascalCase, entityPascalCase);            
            return exitCode;
        }

        public void CreateApi(string microserviceDirectoryName, string namePascalCase, string entityPascalCase) {
            string apiDirectoryName = $"{microserviceDirectoryName}//{namePascalCase}";
            CreateDirectory(apiDirectoryName);

            _fileWriter.WriteAllLines($"{apiDirectoryName}//{namePascalCase}.csproj", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroService.MicroService.csproj", BluePrintType.MicroService), namePascalCase, entityPascalCase));

            _fileWriter.WriteAllLines($"{apiDirectoryName}//ApiConfiguration.cs", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroService.ApiConfiguration.csharp", BluePrintType.MicroService), namePascalCase, entityPascalCase));
            _fileWriter.WriteAllLines($"{apiDirectoryName}//UnityConfiguration.cs", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroService.UnityConfiguration.csharp", BluePrintType.MicroService), namePascalCase, entityPascalCase));
            _fileWriter.WriteAllLines($"{apiDirectoryName}//packages.config", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroService.packages.config", BluePrintType.MicroService), namePascalCase, entityPascalCase));
            _fileWriter.WriteAllLines($"{apiDirectoryName}//App.config", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroService.App.config", BluePrintType.MicroService), namePascalCase, entityPascalCase));
            _fileWriter.WriteAllLines($"{apiDirectoryName}//install-packages.txt", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroService.install-packages.txt", BluePrintType.MicroService), namePascalCase, entityPascalCase));

            var folders = new List<string>() {
                "Controllers",
                "App_Start",
                "Authentication",
                "Clients",
                "Configuration",
                "Data",
                "Dtos",
                "Filters",
                "Infrastructure",
                "Migrations",
                "Models",
                "Properties",
                "Services",
                "Utilities" };
            
            foreach(var folder in folders)
            {
                var folderPath = $"{apiDirectoryName}//{folder}";

                CreateDirectory(folderPath);

                if(folder == "Controllers")
                {
                    _fileWriter.WriteAllLines($"{folderPath}//{entityPascalCase}Controller.cs", _templateProcessor.ProcessTemplate(_templateManager.Get($"MicroService.{folder}.EntityController.csharp", BluePrintType.MicroService), namePascalCase, entityPascalCase));
                    
                }

                if (folder == "App_Start")
                {
                    WriteFile(folderPath, "WebApiUnityActionFilterProvider.cs", "MicroService", folder, "WebApiUnityActionFilterProvider.csharp", namePascalCase, entityPascalCase);
                }

                if (folder == "Authentication")
                {
                    WriteFile(folderPath, "JwtOptions.cs", "MicroService", folder, "JwtOptions.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "JwtWriterFormat.cs", "MicroService", folder, "JwtWriterFormat.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "OAuthOptions.cs", "MicroService", folder, "OAuthOptions.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "OAuthProvider.cs", "MicroService", folder, "OAuthProvider.csharp", namePascalCase, entityPascalCase);
                }

                if (folder == "Clients")
                {
                    WriteFile(folderPath, "IdentityClient.cs", "MicroService", folder, "IdentityClient.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "IIdentityClient.cs", "MicroService", folder, "IIdentityClient.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "ISearchClient.cs", "MicroService", folder, "ISearchClient.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "SearchClient.cs", "MicroService", folder, "SearchClient.csharp", namePascalCase, entityPascalCase);
                }

                if (folder == "Configuration")
                {
                    WriteFile(folderPath, "AuthConfiguration.cs", "MicroService", folder, "AuthConfiguration.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "IAuthConfiguration.cs", "MicroService", folder, "IAuthConfiguration.csharp", namePascalCase, entityPascalCase);

                }

                if (folder == "Data")
                {
                    WriteFile(folderPath, "DataContext.cs", "MicroService", folder, "DataContext.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "EFRepository.cs", "MicroService", folder, "EFRepository.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "IDbContext.cs", "MicroService", folder, "IDbContext.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "IRepository.cs", "MicroService", folder, "IRepository.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "IRepositoryProvider.cs", "MicroService", folder, "IRepositoryProvider.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "IUow.cs", "MicroService", folder, "IUow.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "RepositoryProvider.cs", "MicroService", folder, "RepositoryProvider.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "RepositoryFactories.cs", "MicroService", folder, "RepositoryFactories.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "Uow.cs", "MicroService", folder, "Uow.csharp", namePascalCase, entityPascalCase);
                }

                if (folder == "Dtos")
                {
                    WriteFile(folderPath, $"{entityPascalCase}AddOrUpdateRequestDto.cs", "MicroService", folder, "EntityAddOrUpdateRequestDto.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, $"{entityPascalCase}AddOrUpdateResponseDto.cs", "MicroService", folder, "EntityAddOrUpdateResponseDto.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, $"{entityPascalCase}Dto.cs", "MicroService", folder, "EntityDto.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "RegistrationRequestDto.cs", "MicroService", folder, "RegistrationRequestDto.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "RegistrationResponseDto.cs", "MicroService", folder, "RegistrationResponseDto.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "TokenDto.cs", "MicroService", folder, "TokenDto.csharp", namePascalCase, entityPascalCase);
                }

                if (folder == "Filters")
                {
                    WriteFile(folderPath, "HandleError.cs", "MicroService", folder, "HandleError.csharp", namePascalCase, entityPascalCase);                    
                }

                if (folder == "Infrastructure")
                {
                    WriteFile(folderPath, "Constants.cs", "MicroService", folder, "Constants.csharp", namePascalCase, entityPascalCase);
                }

                if (folder == "Migrations")
                {
                    WriteFile(folderPath, "Configuration.cs", "MicroService", folder, "Configuration.csharp", namePascalCase, entityPascalCase);
                }

                if (folder == "Models")
                {
                    WriteFile(folderPath, $"{entityPascalCase}.cs", "MicroService", folder, "Entity.csharp", namePascalCase, entityPascalCase);
                }

                if (folder == "Properties")
                {
                    WriteFile(folderPath, "AssemblyInfo.cs", "MicroService", folder, "AssemblyInfo.csharp", namePascalCase, entityPascalCase);
                }

                if (folder == "Services")
                {
                    WriteFile(folderPath, $"{entityPascalCase}Service.cs", "MicroService", folder, "EntityService.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, $"I{entityPascalCase}Service.cs", "MicroService", folder, "IEntityService.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "Cache.cs", "MicroService", folder, "Cache.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "ICache.cs", "MicroService", folder, "ICache.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "CacheProvider.cs", "MicroService", folder, "CacheProvider.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "ICacheProvider.cs", "MicroService", folder, "ICacheProvider.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "EncryptionService.cs", "MicroService", folder, "EncryptionService.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "IEncryptionService.cs", "MicroService", folder, "IEncryptionService.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "IdentityService.cs", "MicroService", folder, "IdentityService.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "IIdentityService.cs", "MicroService", folder, "IIdentityService.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "MemoryCache.cs", "MicroService", folder, "MemoryCache.csharp", namePascalCase, entityPascalCase);
                }

                if (folder == "Utilities")
                {
                    WriteFile(folderPath, "ILogger.cs", "MicroService", folder, "ILogger.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "ILoggerFactory.cs", "MicroService", folder, "ILoggerFactory.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "ILoggerProvider.cs", "MicroService", folder, "ILoggerProvider.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "Logger.cs", "MicroService", folder, "Logger.csharp", namePascalCase, entityPascalCase);
                    WriteFile(folderPath, "LoggerFactory.cs", "MicroService", folder, "LoggerFactory.csharp", namePascalCase, entityPascalCase);
                }
            }

        }

        public void WriteFile(string folderPath, string outputFileName, string projectName, string folderName, string templateName, string namePascalCase, string entityPascalCase)
        {
            _fileWriter.WriteAllLines($"{folderPath}//{outputFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get($"{projectName}.{folderName}.{templateName}", BluePrintType.MicroService), namePascalCase, entityPascalCase));
        }

        public void CreateWeb(string microserviceDirectoryName, string namePascalCase, string entityPascalCase)
        {
            string webDirectoryName = $"{microserviceDirectoryName}//{namePascalCase}.Web";
            CreateDirectory(webDirectoryName);

            var folders = new List<string>() { "Properties" };

            foreach (var folder in folders)
            {
                var folderPath = $"{webDirectoryName}//{folder}";

                CreateDirectory(folderPath);

                if (folder == "Properties")
                {
                    WriteFile(folderPath, "AssemblyInfo.cs", "MicroServiceWeb", folder, "AssemblyInfo.csharp", namePascalCase, entityPascalCase);
                }
            }

            _fileWriter.WriteAllLines($"{webDirectoryName}//{namePascalCase}.Web.csproj", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroServiceWeb.MicroServiceWeb.csproj", BluePrintType.MicroService), namePascalCase, entityPascalCase));
            _fileWriter.WriteAllLines($"{webDirectoryName}//index.html", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroServiceWeb.index.html", BluePrintType.MicroService), namePascalCase, entityPascalCase));
            _fileWriter.WriteAllLines($"{webDirectoryName}//packages.config", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroServiceWeb.packages.config", BluePrintType.MicroService), namePascalCase, entityPascalCase));
            _fileWriter.WriteAllLines($"{webDirectoryName}//Web.config", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroServiceWeb.Web.config", BluePrintType.MicroService), namePascalCase, entityPascalCase));
            _fileWriter.WriteAllLines($"{webDirectoryName}//install-packages.txt", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroServiceWeb.install-packages.txt", BluePrintType.MicroService), namePascalCase, entityPascalCase));
            _fileWriter.WriteAllLines($"{webDirectoryName}//Startup.cs", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroServiceWeb.Startup.csharp", BluePrintType.MicroService), namePascalCase, entityPascalCase));

            _generateAppCommand.Run(namePascalCase, entityPascalCase, webDirectoryName);

        }

        public void CreateTests(string microserviceDirectoryName, string namePascalCase, string entityPascalCase)
        {
            string apiDirectoryName = $"{microserviceDirectoryName}//{namePascalCase}.Tests";
            CreateDirectory(apiDirectoryName);

            var folders = new List<string>() { "Commands", "Properties" };

            foreach (var folder in folders)
            {
                var folderPath = $"{apiDirectoryName}//{folder}";

                CreateDirectory(folderPath);

                if (folder == "Properties")
                {
                    WriteFile(folderPath, "AssemblyInfo.cs", "MicroServiceTests", folder, "AssemblyInfo.csharp", namePascalCase, entityPascalCase);
                }
            }
        }

        public void CreateCli(string microserviceDirectoryName, string namePascalCase, string entityPascalCase)
        {
            string apiDirectoryName = $"{microserviceDirectoryName}//{namePascalCase}.Cli";
            CreateDirectory(apiDirectoryName);

            var folders = new List<string>() { "Commands", "Properties" };

            foreach (var folder in folders)
            {
                var folderPath = $"{apiDirectoryName}//{folder}";

                CreateDirectory(folderPath);

                if (folder == "Properties")
                {
                    WriteFile(folderPath, "AssemblyInfo.cs", "MicroServiceCli", folder, "AssemblyInfo.csharp", namePascalCase, entityPascalCase);
                }
            }

            _fileWriter.WriteAllLines($"{apiDirectoryName}//{namePascalCase}.Cli.csproj", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroServiceCli.MicroServiceCli.csproj", BluePrintType.MicroService), namePascalCase, entityPascalCase));
            _fileWriter.WriteAllLines($"{apiDirectoryName}//Program.cs", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroServiceCli.Program.csharp", BluePrintType.MicroService), namePascalCase, entityPascalCase));
            _fileWriter.WriteAllLines($"{apiDirectoryName}//SelfHostOptions.cs", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroServiceCli.SelfHostOptions.csharp", BluePrintType.MicroService), namePascalCase, entityPascalCase));
            _fileWriter.WriteAllLines($"{apiDirectoryName}//Startup.cs", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroServiceCli.StartUp.csharp", BluePrintType.MicroService), namePascalCase, entityPascalCase));
            _fileWriter.WriteAllLines($"{apiDirectoryName}//packages.config", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroServiceCli.packages.config", BluePrintType.MicroService), namePascalCase, entityPascalCase));
            _fileWriter.WriteAllLines($"{apiDirectoryName}//App.config", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroServiceCli.App.config", BluePrintType.MicroService), namePascalCase, entityPascalCase));
            _fileWriter.WriteAllLines($"{apiDirectoryName}//install-packages.txt", _templateProcessor.ProcessTemplate(_templateManager.Get("MicroServiceCli.install-packages.txt", BluePrintType.MicroService), namePascalCase, entityPascalCase));            
        }
    }
}
