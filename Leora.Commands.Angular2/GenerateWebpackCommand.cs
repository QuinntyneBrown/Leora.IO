using System;
using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Services.Contracts;
using Leora.Models;

namespace Leora.Commands.Angular2
{
    public class GenerateWebpackCommand: BaseCommand<GenerateWebpackOptions>, IGenerateWebpackCommand
    {
        public GenerateWebpackCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateWebpackOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            _fileWriter.WriteAllLines($"{directory}//webpack.config.js", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.JavaScript, "Angular2Webpack", BluePrintType.Angular2), name));

            try { _projectManager.Process(directory, $"webpack.config.js", FileType.JavaScript); }
            catch { }

            return exitCode;
        }
    }
}
