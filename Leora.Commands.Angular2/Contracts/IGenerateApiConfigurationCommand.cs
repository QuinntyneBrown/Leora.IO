using Leora.Commands.Contracts;

namespace Leora.Commands.Angular2.Contracts
{
    public interface IGenerateApiConfigurationCommand: ICommand
    {
        int Run(string name, string directory);
    }
}
