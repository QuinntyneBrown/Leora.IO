using Leora.Commands.Contracts;

namespace Leora.Commands.Angular1.Contracts
{
    public interface IGenerateUIComponentCommand: ICommand
    {
        int Run(string directory, string name);
    }
}
