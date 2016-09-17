using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Models;
using Leora.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateUploadHandlersCommand: BaseCommand<GenerateUploadHandlersOptions>, IGenerateUploadHandlersCommand
    {
        public GenerateUploadHandlersCommand(IFileWriter fileWriter, ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, INamespaceManager namespaceManager, IProjectManager projectManager)
            :base(fileWriter,templateManager,templateProcessor, namingConventionConverter, namespaceManager, projectManager) { }

        public override int Run(GenerateUploadHandlersOptions options) => Run(options.NameSpace, options.Directory, options.Name, options.RootNamespace);

        public int Run(string namespacename, string directory, string name, string rootNamespace)
        {
            int exitCode = 1;

            System.IO.Directory.CreateDirectory($"{directory}//UploadHandlers");

            directory = $"{directory}//UploadHandlers";
            namespacename = $"{namespacename}.UploadHandlers";

            var streamHelper = "StreamHelper.cs";
            var fileMultipartFormDataStreamProvider = "FileMultipartFormDataStreamProvider.cs";
            var inMemoryMultipartFormDataStreamProvider = "InMemoryMultipartFormDataStreamProvider.cs";

            _fileWriter.WriteAllLines($"{directory}//{streamHelper}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "ApiStreamHelper", BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));
            _fileWriter.WriteAllLines($"{directory}//{fileMultipartFormDataStreamProvider}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "ApiFileMultipartFormDataStreamProvider", BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));
            _fileWriter.WriteAllLines($"{directory}//{inMemoryMultipartFormDataStreamProvider}", _templateProcessor.ProcessTemplate(_templateManager.Get(FileType.CSharp, "ApiInMemoryMultipartFormDataStreamProvider", BluePrintType.AspNetWebApi2), name, namespacename, rootNamespace));

            try {
                _projectManager.Process(directory, streamHelper, FileType.CSharp);
                _projectManager.Process(directory, fileMultipartFormDataStreamProvider, FileType.CSharp);
                _projectManager.Process(directory, inMemoryMultipartFormDataStreamProvider, FileType.CSharp);
            }
            catch
            {

            }
            return exitCode;
        }
    }
}
