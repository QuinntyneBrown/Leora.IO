using System;
using Leora.Commands.VisualStudio.Contracts;
using Leora.Commands.VisualStudio.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;
using System.IO;
using System.Net;

namespace Leora.Commands.VisualStudio
{
    public class GenerateProjectCommand : BaseCommand<GenerateProjectOptions>, IGenerateProjectCommand
    {
        public GenerateProjectCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, INamespaceManager namespaceManager)
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter,namespaceManager) {
        }
        public override int Run(GenerateProjectOptions options) => Run(options.Name, options.Directory);        

        public int Run(string name, string directory)
        {
            int exitCode = 1;

            return exitCode;
        }
    }
}
