using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using static System.IO.File;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateDtosCommand : BaseCommand<GenerateDtosOptions>, IGenerateDtosCommand
    {
        public GenerateDtosCommand(ITemplateManager templateManager, 
            IDotNetTemplateProcessor templateProcessor, 
            INamingConventionConverter namingConventionConverter, 
            INamespaceManager namespaceManager,
            IProjectManager projectManager)
            : base(templateManager, templateProcessor, namingConventionConverter, namespaceManager, projectManager) { }

        public override int Run(GenerateDtosOptions options) => Run(options.NameSpace, options.Directory, options.Name, options.RootNamespace);

        public int Run(string namespacename, string directory, string name, string rootNamespace)
        {
            int exitCode = 1;
            var pascalCaseName = _namingConventionConverter.Convert(NamingConvention.PascalCase, name);
            var dtoName = $"{pascalCaseName}Dto";
            var addOrUpdateRequestName = $"{pascalCaseName}AddOrUpdateRequestDto.cs";
            var addOrUpdateResponseName = $"{pascalCaseName}AddOrUpdateResponseDto.cs";

            WriteAllLines($"{directory}//{dtoName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));
            WriteAllLines($"{directory}//{addOrUpdateRequestName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));
            WriteAllLines($"{directory}//{addOrUpdateResponseName}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));

            _projectManager.Add(directory, $"{dtoName}", FileType.CSharp);
            _projectManager.Add(directory, $"{addOrUpdateRequestName}", FileType.CSharp);
            _projectManager.Add(directory, $"{addOrUpdateResponseName}", FileType.CSharp);

            return exitCode;
        }
    }
}
