namespace Leora.Services.Contracts
{
    public interface IFileWriter
    {
        void WriteAllLines(string path, string[] lines);
    }
}
