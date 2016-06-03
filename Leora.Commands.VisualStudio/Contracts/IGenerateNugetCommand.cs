using Leora.Commands.Contracts;

namespace Leora.Commands.VisualStudio.Contracts
{
    public interface IGenerateNugetCommand : ICommand
    {
        int Run(string name, string directory);
    }
}
