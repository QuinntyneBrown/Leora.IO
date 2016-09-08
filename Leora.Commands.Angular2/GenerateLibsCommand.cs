using Leora.Commands.Angular2.Contracts;
using Leora.Commands.Angular2.Options;
using Leora.Models;
using Leora.Services.Contracts;

namespace Leora.Commands.Angular2
{
    public class GenerateLibsCommand: BaseCommand<GenerateLibsOptions>, IGenerateLibsCommand
    {
        public GenerateLibsCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateLibsOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            var libsDirectory = $"{directory}//lib";
            System.IO.Directory.CreateDirectory(libsDirectory);             
            _fileWriter.WriteAllLines($"{libsDirectory}//jquery.min.js", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.JavaScript, "Angular2JQuery", BluePrintType.Angular2), name));
            _fileWriter.WriteAllLines($"{libsDirectory}//slick.min.js", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.JavaScript, "Angular2Slick", BluePrintType.Angular2), name));

            try {
                _projectManager.Process(libsDirectory, "jquery.min.js", FileType.JavaScript);
                _projectManager.Process(libsDirectory, "slick.min.js", FileType.JavaScript);
            }
            catch { }

            return exitCode;
        }
    }
}
