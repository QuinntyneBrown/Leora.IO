using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Leora.Services
{
    public class ZipFileManager: IZipFileManager
    {

        public byte[] CreateZipFile(List<ZipFileItem>  zipFileItems)
        {
            using (var compressedFileStream = new MemoryStream())
            {
                //Create an archive and store the stream in memory.
                using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Update, false))
                {
                    foreach (var item in zipFileItems)
                    {
                        //Create a zip entry for each attachment
                        var zipEntry = zipArchive.CreateEntry(item.FullName);

                        //Get the stream of the attachment
                        using (var originalFileStream = new MemoryStream(item.Bytes))
                        {
                            using (var zipEntryStream = zipEntry.Open())
                            {
                                //Copy the attachment stream to the zip entry stream
                                originalFileStream.CopyTo(zipEntryStream);
                            }
                        }
                    }

                }

                return compressedFileStream.ToArray();
            }
        }
    }
}
