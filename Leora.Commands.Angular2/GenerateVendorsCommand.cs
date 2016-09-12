using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Services.Contracts;
using Leora.Models;
using System;

namespace Leora.Commands.Angular2
{
    public class GenerateVendorsCommand : BaseCommand<GenerateVendorsOptions>, IGenerateVendorsCommand
    {
        public GenerateVendorsCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateVendorsOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            _fileWriter.WriteAllLines($"{directory}//vendor.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2Vendors", BluePrintType.Angular2), name));

            try { _projectManager.Process(directory, $"vendor.ts", FileType.TypeScript); }
            catch { }

            return exitCode;
        }
    }
}
