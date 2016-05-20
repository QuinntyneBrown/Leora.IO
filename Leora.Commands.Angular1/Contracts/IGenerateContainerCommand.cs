using Leora.Commands.Contracts;

namespace Leora.Commands.Angular1.Contracts
{
    public interface IGenerateContainerCommand : ICommand
    {
        int Run(string name, string directory);
    }
}