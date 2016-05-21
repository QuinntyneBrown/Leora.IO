using System;
using Leora.Services.Contracts;
using System.Xml.Linq;
using System.Linq;
using System.IO;

namespace Leora.Services
{
    public class ProjectManager : IProjectManager
    {
        public void Add(string currentDirectory, string fileName)
        {
            var relativePathAndProjFile = GetRelativePathAndProjFile($"{currentDirectory}//{fileName}");
            var csproj = XDocument.Load(relativePathAndProjFile.ProjFile);
            XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";
            var itemGroups = csproj.Descendants(msbuild + "ItemGroup");
            var itemGroup = itemGroups.FirstOrDefault(x => x.Descendants(msbuild + "Compile").Any());
            var item = new XElement(msbuild + "Compile");
            item.SetAttributeValue("Include", relativePathAndProjFile.RelativePath);
            itemGroup.Add(item);
            csproj.Save(relativePathAndProjFile.ProjFile);
        }
        
        public RelativePathAndProjFile GetRelativePathAndProjFile(string fullFilePath, int nestingLevel = 0)
        {
            var directories = Path.GetDirectoryName(fullFilePath).Split(Path.DirectorySeparatorChar);
            var newDirectories = directories.Take(directories.Length - nestingLevel).ToArray();
            var final = String.Join(Path.DirectorySeparatorChar.ToString(), newDirectories);
            var projectFiles = System.IO.Directory.GetFiles(final, "*.csproj");

            if (projectFiles.FirstOrDefault() != null)
            {
                var x = directories.Skip(directories.Length - nestingLevel).Take(final.Split(Path.DirectorySeparatorChar).Length);
                return new RelativePathAndProjFile(Path.GetFileName(fullFilePath),string.Join(Path.DirectorySeparatorChar.ToString(), x), projectFiles.First());                
            }
            else
            {
                nestingLevel = nestingLevel + 1;
                return GetRelativePathAndProjFile(fullFilePath, nestingLevel);
            }
        }
    }

    public class RelativePathAndProjFile
    {
        public RelativePathAndProjFile(string fileName, string relativePath, string projFile)
        {
            this.RelativePath = $@"{relativePath}\{fileName}";
            this.ProjFile = projFile;
        }

        public string RelativePath { get; set; }
        public string ProjFile { get; set; }
    }
}
