using System;
using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Services.Contracts;
using Leora.Models;

namespace Leora.Commands.Angular2
{
    public class GenerateIndexCommand: BaseCommand<GenerateIndexOptions>, IGenerateIndexCommand
    {
        public GenerateIndexCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateIndexOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var entityNameSnakeCase = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            _fileWriter.WriteAllLines($"{directory}//index.html", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Html, "Angular2Index", BluePrintType.Angular2), name));
            return exitCode;
        }
    }

}
