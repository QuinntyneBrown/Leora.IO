using Leora.Models;

namespace Leora.Services.Contracts
{
    public interface ILeoraJSONFileManager
    {
        LeoraJSONFile GetLeoraJSONFile(string path);
    }
}
