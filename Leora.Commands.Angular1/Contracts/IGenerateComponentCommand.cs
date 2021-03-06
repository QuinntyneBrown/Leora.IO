﻿using Leora.Commands.Contracts;

namespace Leora.Commands.Angular1.Contracts
{
    public interface IGenerateComponentCommand : ICommand
    {
        int Run(string name, string directory, bool framework = false);
    }
}
