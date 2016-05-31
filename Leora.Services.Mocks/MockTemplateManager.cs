using Leora.Services.Contracts;
using System.Collections.Generic;
using Leora.Models;

namespace Leora.Services.Mocks
{
    public class MockTemplateManager : ITemplateManager
    {
        public string[] Get(FileType fileType, string name, string framework = null)
        {
            return new List<string>().ToArray();
        }
    }
}
