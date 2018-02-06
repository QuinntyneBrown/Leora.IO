using Leora.Services.Contracts;
using System.Linq;
using static System.String;
using static System.Xml.Linq.XDocument;
using static System.IO.Directory;
using static System.IO.Path;
using System.Xml.Linq;
using System.Collections.Generic;
using Leora.Models;
using System.IO;

namespace Leora.Services
{
    public class NamespaceManager: INamespaceManager
    {
        protected readonly XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";
        protected readonly INamingConventionConverter _namingConventionConverter;
        protected readonly ILeoraJSONFileManager _leoraJSONFileManager;
        public NamespaceManager(INamingConventionConverter namingConventionConverter, ILeoraJSONFileManager leoraJSONFileManager)
        {
            _namingConventionConverter = namingConventionConverter;
            _leoraJSONFileManager = leoraJSONFileManager;
        }

        public NamespaceManager()
        {
            _namingConventionConverter = new NamingConventionConverter();
            _leoraJSONFileManager = new LeoraJSONFileManager();
        }

        public FileNamespace GetNamespace(string path)
        {
            var rootNamespace = _leoraJSONFileManager.GetLeoraJSONFile(path, -1).RootNamespace;

            var projectPath = GetProjectPath(path);
            
            var subNamespaces = GetSubNamespaces(path, projectPath);

            if(subNamespaces.Count() < 1)
                return new FileNamespace() { Namespace = rootNamespace, RootNamespace = rootNamespace };

            return new FileNamespace() { Namespace = $"{rootNamespace}.{Join(".",subNamespaces)}", RootNamespace = rootNamespace };            
        }

        public string GetProjectPath(string path, int depth = 0)
        {
            var directories = path.Split(DirectorySeparatorChar);
            
            if (directories.Length <= depth)
                return null;

            var newDirectories = directories.Take(directories.Length - depth);
            var computedPath = Join(DirectorySeparatorChar.ToString(), newDirectories);
            var projectFiles = GetFiles(computedPath, "*.csproj");
            depth = depth + 1;
            return (projectFiles.FirstOrDefault() != null) ? projectFiles.First() : GetProjectPath(path, depth);
        }

        public List<string> GetSubNamespaces(string path, string projectPath)
        {
            var pathDirectories = path.Split(DirectorySeparatorChar);
            var skip = GetDirectoryName(projectPath).Split(DirectorySeparatorChar).Count();
            var subNamespaces = pathDirectories.Skip(skip).Take(pathDirectories.Length - skip).ToList();
            List<string> subNamespacesPascalCase = new List<string>();
            foreach(var subNamespace in subNamespaces)
            {
                subNamespacesPascalCase.Add(_namingConventionConverter.Convert(NamingConvention.PascalCase, subNamespace));
            }
            return subNamespacesPascalCase;
        }

        public bool IsDirectory(string path) => File.GetAttributes(path).HasFlag(FileAttributes.Directory);
    }
}
