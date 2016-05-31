using Leora.Services.Contracts;
using static System.IO.Directory;
using static System.IO.Path;
using System.Linq;
using static System.String;
using static System.Xml.Linq.XDocument;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Leora.Services
{
    public class NamespaceManager: INamespaceManager
    {
        protected readonly XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";

        public string GetNamespace(string path)
        {
            var namespaces = new List<string>();
            var projectPath = GetProjectPath(path);
            var csproj = Load(projectPath);
            var projectGroups = csproj.Descendants(msbuild + "PropertyGroup");
            var rootNamespaceNode = projectGroups.Descendants(msbuild + "RootNamespace").First();
            namespaces.Add((string)rootNamespaceNode.Value);
            foreach(var ns in GetSubNamespaces(path, projectPath))
                namespaces.Add(ns);
            return Join(".", namespaces);            
        }

        public string GetProjectPath(string path, int depth = 0)
        {
            var directories = GetDirectoryName(path).Split(DirectorySeparatorChar);
            var newDirectories = directories.Take(directories.Length - depth).ToArray();
            var computedPath = Join(DirectorySeparatorChar.ToString(), newDirectories);
            var projectFiles = GetFiles(computedPath, "*.csproj");
            if (projectFiles.FirstOrDefault() != null)
                return projectFiles.First();
            depth = depth + 1;
            return GetProjectPath(path, depth);
        }

        public List<string> GetSubNamespaces(string path, string projectPath)
        {
            var pathDirectories = GetDirectoryName(path).Split(DirectorySeparatorChar);
            var skip = GetDirectoryName(projectPath).Split(DirectorySeparatorChar).Count();
            return pathDirectories.Skip(skip).Take(pathDirectories.Length - skip).ToList();
        }
    }
}
