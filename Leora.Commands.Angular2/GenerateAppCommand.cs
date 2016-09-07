using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.Commands.Angular2
{
    public class GenerateAppCommand:BaseCommand<GenerateAppOptions>, IGenerateAppCommand
    {
        protected readonly IGenerateAppPackageJsonCommand _generateAppPackageJsonCommand;

        public GenerateAppCommand(
            ITemplateManager templateManager, 
            ITemplateProcessor templateProcessor, 
            INamingConventionConverter namingConventionConverter, 
            IProjectManager projectManager, 
            IFileWriter fileWriter,
            IGenerateAppPackageJsonCommand generateAppPackageJsonCommand) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) {

            _generateAppPackageJsonCommand = generateAppPackageJsonCommand;
        }

        public override int Run(GenerateAppOptions options) => Run(options.ProjectName, options.Name, options.Directory);

        public int Run(string projectName, string name, string directory)
        {
            var exitCode = 1;

            _generateAppPackageJsonCommand.Run(projectName, directory);


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
