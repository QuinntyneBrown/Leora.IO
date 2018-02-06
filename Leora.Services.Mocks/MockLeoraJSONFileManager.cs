using Leora.Models;
using Leora.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.Services.Mocks
{
    public class MockLeoraJSONFileManager : ILeoraJSONFileManager
    {
        public LeoraJSONFile GetLeoraJSONFile(string path, int depth)
        {
            return new LeoraJSONFile();
        }
    }
}
