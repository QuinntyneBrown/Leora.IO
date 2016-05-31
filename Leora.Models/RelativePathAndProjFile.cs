namespace Leora.Models
{
    public class RelativePathAndProjFile
    {
        public RelativePathAndProjFile(string fileName, string relativePath, string projFile)
        {
            RelativePath = $@"{relativePath}\{fileName}";
            ProjFile = projFile;
        }

        public string RelativePath { get; set; }
        public string ProjFile { get; set; }
    }
}
