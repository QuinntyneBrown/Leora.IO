using Leora.Commands.Contracts;

namespace Leora.Commands.AspNetWebApi2.Contracts
{
    public interface IAspNetWebApi2Command: ICommand
    {
        int Run(string namespacename, string directory, string name, string rootNamespace);
    }
}
