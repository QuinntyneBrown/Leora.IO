﻿using Leora.Commands.Contracts;

namespace Leora.Commands.Angular1.Contracts
{
    public interface IGeneratePagedListCommand : ICommand
    {
        int Run(string name, string directory);
    }
}
