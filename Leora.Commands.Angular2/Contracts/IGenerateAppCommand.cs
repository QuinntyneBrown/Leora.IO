using Leora.Commands.Contracts;

namespace Leora.Commands.Angular2.Contracts
{
    public interface IGenerateAppCommand: ICommand
    {
        int Run(string projectName, string name, string directory);
    }
}
