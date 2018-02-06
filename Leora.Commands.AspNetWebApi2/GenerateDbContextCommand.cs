using System;
using Leora.Commands.AspNetWebApi2.Contracts;

namespace Leora.Commands.AspNetWebApi2
{
    public class GenerateDbContextCommand : IGenerateDbContextCommand
    {
        public int Run(string[] args)
        {
            throw new NotImplementedException();
        }

        public int Run(string namespacename, string directory, string name, string rootNamespace, string framework)
        {
            throw new NotImplementedException();
        }
    }
}
