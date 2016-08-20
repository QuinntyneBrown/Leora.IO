using Leora.Commands.Contracts;

namespace Leora.Commands.Angular2.Contracts
{
    public interface IGeneratePolyfillsCommand: ICommand
    {
        int Run(string name, string directory);
    }
}
