using Leora.Commands.Contracts;

namespace Leora.Commands.VisualStudio.Contracts
{
    public interface IGenerateProjectCommand : ICommand
    {
        int Run(string name, string directory);
    }
}
