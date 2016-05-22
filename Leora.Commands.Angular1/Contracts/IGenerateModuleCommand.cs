using Leora.Commands.Contracts;

namespace Leora.Commands.Angular1.Contracts
{
    public interface IGenerateModuleCommand: ICommand
    {
        int Run(string name, string directory, bool crud);
    }
}
