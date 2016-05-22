using Leora.Commands.Contracts;

namespace Leora.Commands.Angular1.Contracts
{
    public interface IGenerateServiceCommand: ICommand
    {
        int Run(string name, string directory, bool crud, bool data);
    }
}
