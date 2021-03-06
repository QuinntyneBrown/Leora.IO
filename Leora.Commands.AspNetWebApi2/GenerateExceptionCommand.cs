﻿using Leora.Commands.AspNetWebApi2.Contracts;
using Leora.Commands.AspNetWebApi2.Options;
using Leora.Services.Contracts;
using System;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateExceptionCommand : BaseCommand<GenerateExceptionOptions>, IGenerateExceptionCommand
    {
        public GenerateExceptionCommand(IFileWriter fileWriter, 
            ITemplateManager templateManager, 
            IDotNetTemplateProcessor templateProcessor, 
            INamingConventionConverter namingConventionConverter, 
            INamespaceManager namespaceManager,
            IProjectManager projectManager,
            ILeoraJSONFileManager leoraJSONFileManager)
            :base(fileWriter, templateManager,templateProcessor, namingConventionConverter, namespaceManager, projectManager, leoraJSONFileManager) { }

        public override int Run(GenerateExceptionOptions options)
        {
            throw new NotImplementedException();
        }

        public int Run(string namespacename, string directory, string name, string rootNamespace, string framework)
        {
            throw new NotImplementedException();
        }
    }
}
