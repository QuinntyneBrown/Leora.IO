using Leora.Commands.Contracts;

namespace Leora.Commands.AspNetWebApi2.Contracts
{
    public interface IGenerateNewCommand : ICommand
    {
        int Run(string name, string directory);
    }
}
