using Leora.Commands.Contracts;

namespace Leora.Commands.Angular2.Contracts
{
    public interface IGenerateStateCommand: ICommand
    {
        int Run(string name, string directory);
    }
}
