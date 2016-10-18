using Leora.Models;
using System.Xml.Linq;

namespace Leora.Services.Contracts
{
    public interface IProjectManager
    {
        void Process(string currentDirectory, string fileName, FileType fileType = FileType.TypeScript, bool trace = false);
        XDocument Add(XDocument csproj, string relativePath, FileType fileType = FileType.TypeScript, bool trace = false);
    }
}
