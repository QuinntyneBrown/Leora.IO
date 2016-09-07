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

            return exitCode;
        }
    }
}
