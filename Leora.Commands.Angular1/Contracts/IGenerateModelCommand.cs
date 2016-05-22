﻿using Leora.Commands.Contracts;

namespace Leora.Commands.Angular1.Contracts
{
    public interface IGenerateModelCommand : ICommand
    {
        int Run(string name, string directory);
    }
}
