using Leora.Commands.Contracts;

namespace Leora.Commands.Angular1.Contracts
{
    public interface IGenerateCrudFeatureCommand: ICommand
    {
        int Run(string directory, string name);
    }
}
