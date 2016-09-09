using System.Collections.Generic;

namespace Leora.Services.Contracts
{
    public interface IFileWriter
    {
        void WriteAllLines(string path, string[] lines);
        void WriteAllLines(string path, List<string> lines);
    }
}
