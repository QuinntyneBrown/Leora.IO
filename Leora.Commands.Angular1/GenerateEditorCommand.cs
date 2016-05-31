using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateEditorCommand : BaseCommand<GenerateEditorOptions>, IGenerateEditorCommand
    {

        public GenerateEditorCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager)
        {

        }

        public override int Run(GenerateEditorOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            int exitCode = 1;
            name = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            var typeScriptFileName = $"{name}-editor.component.ts";
            var cssFileName = $"{name}-editor.component.css";
            var htmlFileName = $"{name}-editor.component.html";

            WriteAllLines($"{directory}\\{typeScriptFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.TypeScript, _componentName, BluePrintType.Angular1), name));
            WriteAllLines($"{directory}\\{cssFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Css, _componentName, BluePrintType.Angular1), name));
            WriteAllLines($"{directory}\\{htmlFileName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.Html, _componentName, BluePrintType.Angular1), name));

            _projectManager.Process(directory, typeScriptFileName, FileType.TypeScript);
            _projectManager.Process(directory, cssFileName, FileType.Css);
            _projectManager.Process(directory, htmlFileName, FileType.Html);

            return exitCode;
        }

        private readonly string _componentName = "Angular1EditorComponent";
    }
}
