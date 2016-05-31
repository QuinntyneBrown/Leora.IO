using Leora.Services.Contracts;

namespace Leora.Services
{
    public class FileWriter : IFileWriter
    {
        public void WriteAllLines(string path, string[] lines) 
            => System.IO.File.WriteAllLines(path, lines);
    }
}
