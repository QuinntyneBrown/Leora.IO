﻿using Leora.Commands.Contracts;

namespace Leora.Commands.Angular2.Contracts
{
    public interface IGenerateWebpackCommand: ICommand
    {
        int Run(string name, string directory);
    }
}
