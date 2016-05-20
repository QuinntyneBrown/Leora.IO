namespace Leora.Commands.Contracts
{
    public interface ICommand
    {
        int Run(string[] args);
    }
}
