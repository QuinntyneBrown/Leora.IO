using Leora.Commands.Contracts;

namespace Leora.Commands.VisualStudio.Contracts
{
    public interface IGenerateSolutionCommand : ICommand
    {
        int Run(string name, string directory);
    }
}
