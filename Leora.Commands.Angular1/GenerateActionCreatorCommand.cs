using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateActionCreatorCommand : BaseCommand<GenerateActionCreatorOptions>, IGenerateActionCreatorCommand
    {
        public GenerateActionCreatorCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter)
            :base(templateManager,templateProcessor, namingConventionConverter,projectManager, fileWriter) { }

        public override int Run(GenerateActionCreatorOptions options) => Run(options.Name, options.Directory, options.Crud);

        public int Run(string name, string directory, bool crud)
        {
            int exitCode = 1;
            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            _fileWriter.WriteAllLines($"{directory}//{snakeCaseName}.actions.ts", _templateProcessor.ProcessTemplate(GetTemplate(FileType.TypeScript, GetActionsTemplateName(crud)), name));
            _fileWriter.WriteAllLines($"{directory}//{snakeCaseName}.action-creator.ts", _templateProcessor.ProcessTemplate(GetTemplate(FileType.TypeScript, GetTemplateName(crud)), name));
            _fileWriter.WriteAllLines($"{directory}//{snakeCaseName}.reducers.ts", _templateProcessor.ProcessTemplate(GetTemplate(FileType.TypeScript, GetReducersTemplateName(crud)), name));
            _projectManager.Process(directory, $"{snakeCaseName}.actions.ts", FileType.TypeScript);
            _projectManager.Process(directory, $"{snakeCaseName}.action-creator.ts", FileType.TypeScript);
            _projectManager.Process(directory, $"{snakeCaseName}.reducers.ts", FileType.TypeScript);
            return exitCode;
        }
        public string GetReducersTemplateName(bool crud) => crud ? "Angular1ReducersCrud" : "Angular1Reducers";        
        public string GetTemplateName(bool crud) => crud ? "Angular1ActionCreatorCrud" : "Angular1ActionCreator";
        public string GetActionsTemplateName(bool crud) => crud ? "Angular1ActionsCrud" : "Angular1Actions";
    }
}
