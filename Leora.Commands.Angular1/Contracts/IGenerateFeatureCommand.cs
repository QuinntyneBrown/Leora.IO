using Leora.Commands.Contracts;

namespace Leora.Commands.Angular1.Contracts
{
    public interface IGenerateFeatureCommand: ICommand
    {
        int Run(string directory, string name, bool crud);
    }
}
