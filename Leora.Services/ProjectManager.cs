using System;
using Leora.Services.Contracts;
using System.Xml.Linq;
using System.Linq;
using Leora.Models;
using System.Collections.Generic;
using static System.String;
using static System.IO.Directory;
using static System.IO.Path;

namespace Leora.Services
{
    public class ProjectManager : IProjectManager
    {
        protected readonly XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";

        public void Process(string currentDirectory, string fileName, FileType fileType = FileType.TypeScript)
        {
            var relativePathAndProjFile = GetRelativePathAndProjFile($"{currentDirectory}//{fileName}");
            if (relativePathAndProjFile != null)
            {
                var csproj = Add(XDocument.Load(relativePathAndProjFile.ProjFile), relativePathAndProjFile.RelativePath, fileType);
                csproj.Save(relativePathAndProjFile.ProjFile);
            }
        }

        public XDocument Add(XDocument csproj, string relativePath, FileType fileType = FileType.TypeScript)
        {            
            var itemGroup = GetItemGroup(fileType, csproj.Descendants(msbuild + "ItemGroup"));

            Console.WriteLine("Got Here");

            if (itemGroup == null)
            {
                AddItemGroup(fileType, csproj);
                itemGroup = csproj.Descendants(msbuild + "ItemGroup").Last();
            }

            var item = GetItem(fileType);
            item.SetAttributeValue("Include", relativePath);
            itemGroup.Add(item);
            return csproj;
        }

        public RelativePathAndProjFile GetRelativePathAndProjFile(string fullFilePath, int depth = 0)
        {
            var directories = GetDirectoryName(fullFilePath).Split(DirectorySeparatorChar);

            if (directories.Length < depth)
                return null;

            var newDirectories = directories.Take(directories.Length - depth).ToArray();
            var computedPath = Join(DirectorySeparatorChar.ToString(), newDirectories);
            var projectFiles = GetFiles(computedPath, "*.csproj");

            if (projectFiles.FirstOrDefault() != null)
            {
                var x = directories.Skip(directories.Length - depth).Take(computedPath.Split(DirectorySeparatorChar).Length);
                return new RelativePathAndProjFile(GetFileName(fullFilePath),Join(DirectorySeparatorChar.ToString(), x), projectFiles.First());                
            }
            else
            {
                depth = depth + 1;
                return GetRelativePathAndProjFile(fullFilePath, depth);
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

        public XDocument AddItemGroup(FileType fileType, XDocument csproj)
        {
            csproj.Root.Add(new XElement(msbuild + "ItemGroup"));
            return csproj;
        }
    }
}
