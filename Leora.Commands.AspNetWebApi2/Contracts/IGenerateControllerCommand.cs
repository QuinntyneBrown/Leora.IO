﻿using Leora.Commands.Contracts;

namespace Leora.Commands.AspNetWebApi2.Contracts
{
    public interface IGenerateControllerCommand: ICommand
    {
        int Run(string namespacename, string directory, string name, string rootNamespace, bool trace = true, bool cqrs = false);
    }
}
