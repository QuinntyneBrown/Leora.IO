using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Services.Contracts;
using System;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateExceptionCommand : BaseCommand<GenerateExceptionOptions>, IGenerateExceptionCommand
    {
        public GenerateExceptionCommand(ITemplateManager templateManager, 
            IDotNetTemplateProcessor templateProcessor, 
            INamingConventionConverter namingConventionConverter, 
            INamespaceManager namespaceManager,
            IProjectManager projectManager)
            :base(templateManager,templateProcessor, namingConventionConverter, namespaceManager, projectManager) { }

        public override int Run(GenerateExceptionOptions options)
        {
            throw new NotImplementedException();
        }

        public int Run(string namespacename, string directory, string name)
        {
            throw new NotImplementedException();
        }
    }
}
