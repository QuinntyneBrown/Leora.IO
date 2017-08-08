using Leora.Commands.Contracts;
using Leora.Commands.CQRS.Core;
using Leora.Models;
using Leora.Services.Contracts;
using System;

namespace Leora.Commands.CQRS
{
    public class GenerateRemovedOptions : BaseOptions
    {
        public GenerateRemovedOptions()
            : base()
        {

        }
    }

    public interface IGenerateRemovedMessageCommand : ICommand
    {

    }

    public class GenerateRemovedMessageCommand : BaseCommand<GenerateRemovedOptions>, IGenerateRemovedMessageCommand
    {
        public GenerateRemovedMessageCommand(ITemplateManager templateManager, IDotNetTemplateProcessor templateProcessor, INamingConventionConverter namingConventionConverter, IProjectManager projectManager, IFileWriter fileWriter, INamespaceManager namespaceManager)
            : base(templateManager, templateProcessor, namingConventionConverter, projectManager, fileWriter, namespaceManager)
        {
        }
        public override int Run(GenerateRemovedOptions options) => Run(options.Name, options.Directory);

        public int Run(string name, string directory)
        {
            int exitCode = 1;

            return exitCode;
        }

    }
}
