using Leora.Services.Contracts;
using System.Linq;
using static System.String;
using static System.Xml.Linq.XDocument;
using static System.IO.Directory;
using static System.IO.Path;
using System.Xml.Linq;
using System.Collections.Generic;
using Leora.Models;

namespace Leora.Services
{
    public class NamespaceManager: INamespaceManager
    {
        protected readonly XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";

        public FileNamespace GetNamespace(string path)
        {
            var projectPath = GetProjectPath(path);
            var projectGroups = Load(projectPath).Descendants(msbuild + "PropertyGroup");
            var rootNamespace = projectGroups.Descendants(msbuild + "RootNamespace").First().Value;            
            return new FileNamespace() { Namespace = $"{rootNamespace}.{Join(".",GetSubNamespaces(path,projectPath))}", RootNamespace = rootNamespace };
        }

        public string GetProjectPath(string path, int depth = 0)
        {
            var directories = GetDirectoryName(path).Split(DirectorySeparatorChar);

            if (directories.Length < depth)
                return null;

            var newDirectories = directories.Take(directories.Length - depth);
            var computedPath = Join(DirectorySeparatorChar.ToString(), newDirectories);
            var projectFiles = GetFiles(computedPath, "*.csproj");
            depth = depth + 1;
            return (projectFiles.FirstOrDefault() != null) ? projectFiles.First() : GetProjectPath(path, depth);
        }

        public List<string> GetSubNamespaces(string path, string projectPath)
        {
            var pathDirectories = GetDirectoryName(path).Split(DirectorySeparatorChar);
            var skip = GetDirectoryName(projectPath).Split(DirectorySeparatorChar).Count();
            return pathDirectories.Skip(skip).Take(pathDirectories.Length - skip).ToList();
        }
    }
}
