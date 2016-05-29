using Leora.Commands.Contracts;

namespace Leora.Commands.React.Contracts
{
    public interface IGenerateComponentCommand : ICommand
    {
        int Run(string name, string directory);
    }
}
