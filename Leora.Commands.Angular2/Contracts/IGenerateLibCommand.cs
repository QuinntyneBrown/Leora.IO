using Leora.Commands.Contracts;

namespace Leora.Commands.Angular2.Contracts
{
    public interface IGenerateLibCommand : ICommand
    {
        int Run(string projectName, string name, string directory);
    }
}
