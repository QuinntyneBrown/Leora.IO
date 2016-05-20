using Leora.Models;
using System.Collections.Generic;

namespace Leora.Services.Contracts
{
    public interface IZipFileManager
    {
        byte[] CreateZipFile(List<ZipFileItem> zipFileItems);
    }
}
