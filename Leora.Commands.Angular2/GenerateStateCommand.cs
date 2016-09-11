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
    public class GenerateStateCommand: BaseCommand<GenerateStateOptions>, IGenerateStateCommand
    {
        public GenerateStateCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter) 
            :base(templateManager,templateProcessor,namingConventionConverter,projectManager,fileWriter) { }

        public override int Run(GenerateStateOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            var exitCode = 1;
            
            _fileWriter.WriteAllLines($"{directory}//app-state.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2AppState", BluePrintType.Angular2), name));
            _fileWriter.WriteAllLines($"{directory}//initial-state.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2InitialState", BluePrintType.Angular2), name));

            try {
                _projectManager.Process(directory, $"app-state.ts", FileType.TypeScript);
                _projectManager.Process(directory, $"initial-state.ts", FileType.TypeScript);
            }
            catch { }

            return exitCode;
        }
    }
}
