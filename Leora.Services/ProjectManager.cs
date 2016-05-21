using System;
using Leora.Services.Contracts;
using System.Xml.Linq;
using System.Linq;

namespace Leora.Services
{
    public class ProjectManager : IProjectManager
    {
        public void Add(string currentDirectory, string fileName)
        {
            // find the nearest .csproj file

            // keep track of hiearchy

            // open csproj into XML

            // add item to item group

            var csproj = XDocument.Load(GetProjectFullPath(currentDirectory));
            XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";
            var itemGroups = csproj.Descendants(msbuild + "ItemGroup");
            var itemGroup = itemGroups.FirstOrDefault(x => x.Descendants(msbuild + "Compile").Any());
            var item = new XElement(msbuild + "Compile");
            item.SetAttributeValue("Include", GetRelativePath(currentDirectory, fileName));
            itemGroup.Add(item);
            csproj.Save(GetProjectFullPath(currentDirectory));
        }

        public string GetProjectFullPath(string directory)
        {
            //SolutionFolder + @"\SomeProject.csproj"
            return directory;
        }

        public string GetRelativePath(string currentDirectory, string fileName)
        {
            return fileName;
        }
    }
}
