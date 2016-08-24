using Leora.Commands.Contracts;

namespace Leora.Commands.Angular2.Contracts
{
    public interface IGenerateReducerCommand : ICommand
    {
        int Run(string name, string directory);
    }
}
