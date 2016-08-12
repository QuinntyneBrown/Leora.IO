using System;
using Leora.Commands.Angular2.Contracts;
using Leora.Services.Contracts;
using Leora.Models;

namespace Leora.Commands.Angular2
{
    public class GenerateExampleCommand : IGenerateExampleCommand
    {
        private readonly ITemplateManager _templateManager;
        private readonly IGenerateBootstrapCommand _generateBootstrapCommand;
        private readonly IFileWriter _fileWriter;
        private readonly ITemplateProcessor _templateProcessor;

        public GenerateExampleCommand(
            ITemplateManager templateManager,
            IGenerateBootstrapCommand generateBootstrapCommand)
        {
            _templateManager = templateManager;
            _generateBootstrapCommand = generateBootstrapCommand;
        }

        public int Run(string[] args)
        {            
            throw new NotImplementedException();
        }

        public int Run(string name)
        {
            var exitCode = 1;
            _fileWriter.WriteAllLines("example.html", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Html, "Angular2Example", BluePrintType.Angular2), name));
            _fileWriter.WriteAllLines("bootstrap.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular2Bootstrap", BluePrintType.Angular2), name));
            return exitCode;
        }
    }
}
