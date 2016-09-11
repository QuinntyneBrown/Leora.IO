using Leora.Commands.Contracts;

namespace Leora.Commands.Angular2.Contracts
{
    public interface IGenerateRxJSExtensionsCommand: ICommand
    {
        int Run(string name, string directory);
    }
}
