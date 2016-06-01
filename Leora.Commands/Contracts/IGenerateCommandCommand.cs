namespace Leora.Commands.Contracts
{
    public interface IGenerateCommandCommand
    {
        int Run(string name, string directory);
    }
}
