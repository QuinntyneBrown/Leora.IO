using Leora.Services.Contracts;
using System.Collections.Generic;

namespace Leora.Services
{
    public class FileWriter : IFileWriter
    {
        public void WriteAllLines(string path, string[] lines) 
            => System.IO.File.WriteAllLines(path, lines);

        public void WriteAllLines(string path, List<string> lines)
            => System.IO.File.WriteAllLines(path, lines);
    }
}
