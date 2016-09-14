using Leora.Commands.Contracts;

namespace Leora.Commands.AspNetWebApi2.Contracts
{
    public interface IGenerateContentModelCommand: ICommand
    {
        int Run(string namespacename, string directory, string name, string rootNamespace);
    }
}
