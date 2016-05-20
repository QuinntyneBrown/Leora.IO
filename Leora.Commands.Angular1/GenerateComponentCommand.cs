using Leora.Commands.Angular1.Contracts;
using static System.Environment;
using Leora.Services.Contracts;
using static System.IO.File;
using static CommandLine.Parser;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using System.Collections.Generic;

namespace Leora.Commands.Angular1
{
    public class GenerateComponentCommand : IGenerateComponentCommand
    {
        private ITemplateManager _templateManager;
        private ITemplateProcessor _templateProcessor;

        public GenerateComponentCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor)
        {
            _templateProcessor = templateProcessor;
            _templateManager = templateManager;
        }

        public int Run(string[] args)
        {
            var options = new GenerateComponentOptions();
            Default.ParseArguments(args, options);
            return Run(options);
        }

        public int Run(GenerateComponentOptions options)
        {
            int exitCode = 1;
            WriteAllLines($"{options.Directory}/{options.Name}.component.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, "Angular1Component"), options.Name));
            WriteAllLines($"{options.Directory}/{options.Name}.component.css", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Css, "Angular1Component"), options.Name));
            WriteAllLines($"{options.Directory}/{options.Name}.component.html", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Html, "Angular1Component"), options.Name));
            return exitCode;
        }

    }
}
