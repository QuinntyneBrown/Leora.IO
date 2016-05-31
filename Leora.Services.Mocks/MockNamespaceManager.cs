using Leora.Services.Contracts;
using Leora.Models;

namespace Leora.Services.Mocks
{
    public class MockNamespaceManager : INamespaceManager
    {
        public FileNamespace GetNamespace(string path)
        {
            return new FileNamespace();
        }
    }
}
