using Leora.Commands.Contracts;

namespace Leora.Commands.Angular2.Contracts
{
    public interface IGenerateIndexFromFolderCommand: ICommand
    {
        int Run(string name, string directory, bool includeScss = false);
    }
}
