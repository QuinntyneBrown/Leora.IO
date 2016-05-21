using Leora.Models;

namespace Leora.Services.Contracts
{
    public interface IProjectManager
    {
        void Add(string currentDirectory, string fileName, FileType fileType = FileType.TypeScript);
    }
}
