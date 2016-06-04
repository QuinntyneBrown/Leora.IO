using Leora.Commands.Contracts;

namespace Leora.Commands.Contracts
{
    public interface IGenerateMicroserviceCommand : ICommand
    {
        int Run(string name, string directory, string entity);
    }
}
