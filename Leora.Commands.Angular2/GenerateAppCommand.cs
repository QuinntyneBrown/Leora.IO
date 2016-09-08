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
            IGenerateTypingsCommand generateTypingsCommand
            ) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) {

            _generateAppPackageJsonCommand = generateAppPackageJsonCommand;
            _generateLibsCommand = generateLibsCommand;
            _generateWebpackCommand = generateWebpackCommand;
            _generateTsConfigJsonCommand = generateTsConfigJsonCommand;
            _generateTypeDocJsonCommand = generateTypeDocJsonCommand;
            _generateTypingsCommand = generateTypingsCommand;

        }

        public override int Run(GenerateAppOptions options) => Run(options.ProjectName, options.Name, options.Directory);

        public int Run(string projectName, string name, string directory)
        {
            var exitCode = 1;

            _generateAppPackageJsonCommand.Run(projectName, directory);
            _generateLibsCommand.Run(projectName, directory);
            _generateWebpackCommand.Run(projectName, directory);
            _generateTsConfigJsonCommand.Run(projectName, directory);
            _generateTypeDocJsonCommand.Run(projectName, directory);
            _generateTypingsCommand.Run(projectName, directory);

            CreateDirectory($"{directory}\\src");
            CreateDirectory($"{directory}\\src\\app");
            CreateDirectory($"{directory}\\src\\app\\actions");
            CreateDirectory($"{directory}\\src\\app\\components");
            CreateDirectory($"{directory}\\src\\app\\configuration");
            CreateDirectory($"{directory}\\src\\app\\constants");
            CreateDirectory($"{directory}\\src\\app\\helpers");
            CreateDirectory($"{directory}\\src\\app\\models");
            CreateDirectory($"{directory}\\src\\app\\pages");
            CreateDirectory($"{directory}\\src\\app\\pipes");
            CreateDirectory($"{directory}\\src\\app\\routing");
            CreateDirectory($"{directory}\\src\\app\\services");
            CreateDirectory($"{directory}\\src\\app\\store");
            CreateDirectory($"{directory}\\src\\app\\utilities");
            CreateDirectory($"{directory}\\src\\styles");


            
            /*
             * 1. package.json - DONE
             * 2. tsconfig.json
             * 3. typedoc.json
             * 4. typings.json
             * 5. webpack.config.js
             * 
             * -- lib
             *      1. jquery.min.js
             *      2. slick.min.js
             *      3. tinymce.min.js
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
