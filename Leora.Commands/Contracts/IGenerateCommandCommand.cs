using Leora.Commands.Contracts;

namespace Leora.Commands.Contracts
{
    public interface IGenerateCommandCommand: ICommand
    {
        int Run(string name, string directory);
    }
}
