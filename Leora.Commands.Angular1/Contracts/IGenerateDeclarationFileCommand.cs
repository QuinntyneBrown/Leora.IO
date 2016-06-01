using Leora.Commands.Contracts;

namespace Leora.Commands.Angular1.Contracts
{
    public interface IGenerateDeclarationFileCommand : ICommand
    {
        int Run(string name, string directory);
    }
}
