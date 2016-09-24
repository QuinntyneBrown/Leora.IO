using Leora.Commands.Contracts;

namespace Leora.Commands.Angular2.Contracts
{
    public interface IGenerateComponentCommand : ICommand
    {
        int Run(string name, string directory, bool simple = false);
    }
}
