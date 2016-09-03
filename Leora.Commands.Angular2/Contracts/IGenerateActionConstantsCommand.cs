using Leora.Commands.Contracts;

namespace Leora.Commands.Angular2.Contracts
{
    public interface IGenerateActionConstantsCommand: ICommand
    {
        int Run(string name, string directory);
    }
}
