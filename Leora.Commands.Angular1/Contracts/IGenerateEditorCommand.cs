using Leora.Commands.Contracts;

namespace Leora.Commands.Angular1.Contracts
{
    public interface IGenerateEditorCommand: ICommand
    {
        int Run(string director, string name);
    }
}
