using Leora.Models;

namespace Leora.Services.Contracts
{
    public interface INamespaceManager
    {
        FileNamespace GetNamespace(string path);
    }
}
