using System;
using Leora.Services.Contracts;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using Leora.Models;
using System.Collections.Generic;

namespace Leora.Services
{
    public class ProjectManager : IProjectManager
    {
        protected readonly XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";

        public void Add(string currentDirectory, string fileName, FileType fileType = FileType.TypeScript)
        {
            var relativePathAndProjFile = GetRelativePathAndProjFile($"{currentDirectory}//{fileName}");
            var csproj = XDocument.Load(relativePathAndProjFile.ProjFile);            
            var itemGroups = csproj.Descendants(msbuild + "ItemGroup");
            var itemGroup = GetItemGroup(fileType, itemGroups);
            var item = GetItem(fileType);
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

        public XElement GetItem(FileType fileType)
        {
            if(fileType == FileType.TypeScript)
                return new XElement(msbuild + "TypeScriptCompile");

            if (fileType == FileType.Css || fileType == FileType.Html)
                return new XElement(msbuild + "Content");

            return new XElement(msbuild + "Compile");
        }
        public XElement GetItemGroup (FileType fileType, IEnumerable<XElement> itemGroups)
        {
            if (fileType == FileType.TypeScript)
                return itemGroups.FirstOrDefault(x => x.Descendants(msbuild + "TypeScriptCompile").Any());

            if (fileType == FileType.Css || fileType == FileType.Html)
                return itemGroups.FirstOrDefault(x => x.Descendants(msbuild + "Content").Any());

            if (fileType == FileType.CSharp)
                return itemGroups.FirstOrDefault(x => x.Descendants(msbuild + "Compile").Any());

            throw new NotImplementedException();
            
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
