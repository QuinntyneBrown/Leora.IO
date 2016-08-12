using System;
using Leora.Commands.Angular2.Contracts;
using Leora.Services.Contracts;

namespace Leora.Commands.Angular2
{
    public class GenerateExampleCommand : IGenerateExampleCommand
    {
        private readonly ITemplateManager _templateManager;

        public GenerateExampleCommand(ITemplateManager templateManager)
        {
            _templateManager = templateManager;
        }

        public int Run(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
