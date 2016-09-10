using Leora.Commands.Contracts;

namespace Leora.Commands.AspNetWebApi2.Contracts
{
    public interface IGenerateMigrationsConfigurationCommand: ICommand
    {
        int Run(string namespacename, string directory, string name, string rootNamespace);
    }
}
