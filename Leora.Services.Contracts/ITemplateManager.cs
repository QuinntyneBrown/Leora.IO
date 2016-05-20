using Leora.Models;

namespace Leora.Services.Contracts
{
    public interface ITemplateManager
    {
        string[] Get(FileType fileType, string name);
    }
}
