using Leora.Services.Contracts;
using System.Collections.Generic;

namespace Leora.Services.Mocks
{
    public class MockFileWriter : IFileWriter
    {
        public void WriteAllLines(string path, string[] lines)
        {
            
        }

        public void WriteAllLines(string path, List<string> lines)
        {

        }
    }
}
