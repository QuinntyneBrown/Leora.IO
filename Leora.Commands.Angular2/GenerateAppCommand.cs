using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.IO.Directory;

namespace Leora.Commands.Angular2
{
    public class GenerateAppCommand:BaseCommand<GenerateAppOptions>, IGenerateAppCommand
    {
        protected readonly IGenerateAppPackageJsonCommand _generateAppPackageJsonCommand;
        protected readonly IGenerateLibsCommand _generateLibsCommand;
        protected readonly IGenerateWebpackCommand _generateWebpackCommand;
        protected readonly IGenerateTsConfigJsonCommand _generateTsConfigJsonCommand;
        protected readonly IGenerateTypeDocJsonCommand _generateTypeDocJsonCommand;
        protected readonly IGenerateTypingsCommand _generateTypingsCommand;
        protected readonly IGenerateActionsCommand _generateActionsCommand;
        protected readonly IGenerateIndexFromFolderCommand _generateIndexFromFolderCommand;
        protected readonly IGenerateComponentCommand _generateComponentCommand;
        protected readonly IGenerateModuleCommand _generateModuleCommand;
        protected readonly IGenerateServiceCommand _generateServiceCommand;
        protected readonly IGenerateActionConstantsCommand _generateActionConstantsCommand;
        protected readonly IGenerateReducerCommand _generateReducerCommand;
        protected readonly IGenerateModelCommand _generateModelCommand;
        protected readonly IGenerateAppStoreCommand _generateAppStoreCommand;
        protected readonly IGenerateRxJSExtensionsCommand _generateRxJSExtensionsCommand;
        protected readonly IGenerateStateCommand _generateStateCommand;
        protected readonly IGenerateUtilitiesCommand _generateUtilitiesCommand;
        protected readonly IGenerateApiConfigurationCommand _generateApiConfigurationCommand;
        protected readonly IGenerateEnvironmentCommand _generateEnvironmentCommand;

        protected readonly IGeneratePolyfillsCommand _generatePolyfillsCommand;
        protected readonly IGenerateVendorsCommand _generateVendorsCommand;
        protected readonly IGenerateMainCommand _generateMainCommand;

        public GenerateAppCommand(
            ITemplateManager templateManager, 
            ITemplateProcessor templateProcessor, 
            INamingConventionConverter namingConventionConverter, 
            IProjectManager projectManager, 
            IFileWriter fileWriter,
            IGenerateAppPackageJsonCommand generateAppPackageJsonCommand,
            IGenerateLibsCommand generateLibsCommand,
            IGenerateWebpackCommand generateWebpackCommand,
            IGenerateTsConfigJsonCommand generateTsConfigJsonCommand,
            IGenerateTypeDocJsonCommand generateTypeDocJsonCommand,
            IGenerateTypingsCommand generateTypingsCommand,
            IGenerateActionsCommand generateActionsCommand,
            IGenerateIndexFromFolderCommand generateIndexFromFolderCommand,
            IGenerateComponentCommand generateComponentCommand,
            IGenerateModuleCommand generateModuleCommand,
            IGenerateServiceCommand generateServiceCommand,
            IGenerateActionConstantsCommand generateActionConstantsCommand,
            IGenerateReducerCommand generateReducerCommand,
            IGenerateModelCommand generateModelCommand,
            IGenerateAppStoreCommand generateAppStoreCommand,
            IGenerateRxJSExtensionsCommand generateRxJSExtensionsCommand,
            IGenerateStateCommand generateStateCommand,
            IGenerateUtilitiesCommand generateUtilitiesCommand,
            IGenerateApiConfigurationCommand generateApiConfigurationCommand,
            IGenerateEnvironmentCommand generateEnvironmentCommand,
            IGeneratePolyfillsCommand generatePolyfillsCommand,
            IGenerateVendorsCommand generateVendorsCommand,
            IGenerateMainCommand generateMainCommand
            )
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) {

            _generateAppPackageJsonCommand = generateAppPackageJsonCommand;
            _generateLibsCommand = generateLibsCommand;
            _generateWebpackCommand = generateWebpackCommand;
            _generateTsConfigJsonCommand = generateTsConfigJsonCommand;
            _generateTypeDocJsonCommand = generateTypeDocJsonCommand;
            _generateTypingsCommand = generateTypingsCommand;
            _generateActionsCommand = generateActionsCommand;
            _generateIndexFromFolderCommand = generateIndexFromFolderCommand;
            _generateComponentCommand = generateComponentCommand;
            _generateModuleCommand = generateModuleCommand;
            _generateServiceCommand = generateServiceCommand;
            _generateActionConstantsCommand = generateActionConstantsCommand;
            _generateReducerCommand = generateReducerCommand;
            _generateModelCommand = generateModelCommand;
            _generateAppStoreCommand = generateAppStoreCommand;
            _generateRxJSExtensionsCommand = generateRxJSExtensionsCommand;
            _generateStateCommand = generateStateCommand;
            _generateUtilitiesCommand = generateUtilitiesCommand;
            _generateApiConfigurationCommand = generateApiConfigurationCommand;
            _generateEnvironmentCommand = generateEnvironmentCommand;

            _generatePolyfillsCommand = generatePolyfillsCommand;
            _generateVendorsCommand = generateVendorsCommand;
            _generateMainCommand = generateMainCommand;
    }

        public override int Run(GenerateAppOptions options) => Run(options.Name, options.Entity, options.Directory);

        
        public int Run(string name, string entity, string directory)
        {
            var exitCode = 1;
            
            _generateAppPackageJsonCommand.Run(name, directory);
            _generateLibsCommand.Run(name, directory);
            _generateWebpackCommand.Run(name, directory);
            _generateTsConfigJsonCommand.Run(name, directory);
            _generateTypeDocJsonCommand.Run(name, directory);
            _generateTypingsCommand.Run(name, directory);

            _fileWriter.WriteAllLines($"{directory}//index.html", _templateProcessor.ProcessTemplate(_templateManager.Get("Angular2AppIndex.html", BluePrintType.Angular2), name));

            CreateDirectory($"{directory}\\src");

            _generateVendorsCommand.Run(entity, $"{directory}\\src");
            _generatePolyfillsCommand.Run(entity, $"{directory}\\src");
            _generateMainCommand.Run(entity, $"{directory}\\src");

            CreateDirectory($"{directory}\\src\\app");
            _generateComponentCommand.Run("app", $"{directory}\\src\\app");
            _generateModuleCommand.Run("app", $"{directory}\\src\\app");
            _generateEnvironmentCommand.Run("app", $"{directory}\\src\\app");            
            _generateRxJSExtensionsCommand.Run(entity, $"{directory}\\src\\app");
            _generateIndexFromFolderCommand.Run(entity, $"{directory}\\src\\app");

            CreateDirectory($"{directory}\\src\\app\\actions");
            _generateActionsCommand.Run(entity, $"{directory}\\src\\app\\actions");
            _generateModuleCommand.Run("actions", $"{directory}\\src\\app\\actions");
            _generateIndexFromFolderCommand.Run(entity, $"{directory}\\src\\app\\actions");

            CreateDirectory($"{directory}\\src\\app\\components");
            _generateModuleCommand.Run("components", $"{directory}\\src\\app\\components");
            _generateIndexFromFolderCommand.Run(entity, $"{directory}\\src\\app\\components");

            CreateDirectory($"{directory}\\src\\app\\configuration");
            _generateApiConfigurationCommand.Run(entity, $"{directory}\\src\\app\\configuration");
            _generateIndexFromFolderCommand.Run(entity, $"{directory}\\src\\app\\configuration");

            CreateDirectory($"{directory}\\src\\app\\constants");
            _generateActionConstantsCommand.Run(entity, $"{directory}\\src\\app\\constants");
            _generateIndexFromFolderCommand.Run(entity, $"{directory}\\src\\app\\constants");

            CreateDirectory($"{directory}\\src\\app\\helpers");
            _generateModuleCommand.Run("helpers", $"{directory}\\src\\app\\helpers");
            _generateIndexFromFolderCommand.Run(entity, $"{directory}\\src\\app\\helpers");

            CreateDirectory($"{directory}\\src\\app\\models");
            _generateModelCommand.Run(entity, $"{directory}\\src\\app\\models");
            _generateIndexFromFolderCommand.Run(entity, $"{directory}\\src\\app\\models");

            CreateDirectory($"{directory}\\src\\app\\pages");
            _generateComponentCommand.Run("home-page", $"{directory}\\src\\app\\pages");
            _generateIndexFromFolderCommand.Run(entity, $"{directory}\\src\\app\\pages");

            CreateDirectory($"{directory}\\src\\app\\pipes");
            _generateModuleCommand.Run("pipes", $"{directory}\\src\\app\\pipes");
            _generateIndexFromFolderCommand.Run(entity, $"{directory}\\src\\app\\pipes");

            CreateDirectory($"{directory}\\src\\app\\routing");
            _generateModuleCommand.Run("routing", $"{directory}\\src\\app\\routing");
            _generateIndexFromFolderCommand.Run(entity, $"{directory}\\src\\app\\routing");

            CreateDirectory($"{directory}\\src\\app\\services");
            _generateModuleCommand.Run("services", $"{directory}\\src\\app\\services");
            _generateServiceCommand.Run(entity, $"{directory}\\src\\app\\services");
            _generateIndexFromFolderCommand.Run(entity, $"{directory}\\src\\app\\services");

            CreateDirectory($"{directory}\\src\\app\\store");
            _generateModuleCommand.Run("store", $"{directory}\\src\\app\\store");
            _generateReducerCommand.Run(entity, $"{directory}\\src\\app\\store");
            _generateAppStoreCommand.Run(entity, $"{directory}\\src\\app\\store");
            _generateStateCommand.Run(entity, $"{directory}\\src\\app\\store");
            _generateIndexFromFolderCommand.Run(entity, $"{directory}\\src\\app\\store");

            CreateDirectory($"{directory}\\src\\app\\utilities");
            _generateUtilitiesCommand.Run(entity, $"{directory}\\src\\app\\utilities");
            _generateIndexFromFolderCommand.Run(entity, $"{directory}\\src\\app\\utilities");

            CreateDirectory($"{directory}\\src\\styles");



            /*
             * 1. package.json - DONE
             * 2. tsconfig.json - DON
             * 3. typedoc.json - DONE
             * 4. typings.json - DONE
             * 5. webpack.config.js - DONE
             * 
             * -- lib
             *      1. jquery.min.js - DONE
             *      2. slick.min.js - DONE
             *      3. tinymce.min.js - DONE
             * -- src
             *      -- app
             *              -- actions
             *              -- components
             *              -- configuration
             *              -- constants
             *              -- helpers
             *              -- models
             *              -- pages
             *              -- pipes
             *              -- routing
             *              -- services
             *              -- store
             *              -- utilities    
             *      -- styles
             *      1. app.component
             *      2. app.module.ts
             *      3. environment
             *      4. index.ts
             *      5. rxjs-extnesions
             * 
            return exitCode;
            */

            return exitCode;
        }
    }
}
