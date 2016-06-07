using Leora.Commands.Contracts;

namespace Leora.Commands.Contracts
{
    public interface IGenerateWebComponentsCommand : ICommand
    {
        int Run(string name, string directory);
    }
}
