using Leora.Commands.Angular1.Contracts;
using Leora.Commands.Angular1.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.Angular1
{
    public class GenerateActionCreatorCommand : BaseCommand<GenerateActionCreatorOptions>, IGenerateActionCreatorCommand
    {
        public GenerateActionCreatorCommand(ITemplateManager templateManager, ITemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager)
            :base(templateManager,templateProcessor, namingConventionConverter,projectManager) { }

        public override int Run(GenerateActionCreatorOptions options) => Run(options.Name, options.Directory, options.Crud);

        public int Run(string name, string directory, bool crud)
        {
            int exitCode = 1;
            var snakeCaseName = _namingConventionConverter.Convert(NamingConvention.SnakeCase, name);
            WriteAllLines($"{directory}//{snakeCaseName}.actions.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(Leora.Models.FileType.TypeScript, GetTemplateName(crud), "Angular1"), name));
            WriteAllLines($"{directory}//{snakeCaseName}.reducers.ts", _templateProcessor.ProcessTemplate(_templateManager.Get(Leora.Models.FileType.TypeScript, GetReducersTemplateName(crud), "Angular1"), name));

            _projectManager.Add(directory, $"{snakeCaseName}.actions.ts", FileType.TypeScript);
            _projectManager.Add(directory, $"{snakeCaseName}.reducers.ts", FileType.TypeScript);

            return exitCode;
        }

        public string GetReducersTemplateName(bool crud)
        {
            return crud ? "Angular1ReducersCrud"
                : "Angular1Reducers";
        }

        public string GetTemplateName(bool crud)
        {
            return crud ? "Angular1ActionCreatorCrud" 
                : "Angular1ActionCreator";
        }
    }
}
