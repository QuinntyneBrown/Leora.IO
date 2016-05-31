using Leora.Services.Contracts;
using static System.IO.Directory;
using static System.IO.Path;
using System.Linq;
using static System.String;
using static System.Xml.Linq.XDocument;
using System.Xml.Linq;

namespace Leora.Services
{
    public class NamespaceManager: INamespaceManager
    {
        protected readonly XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";

        public string GetNamespace(string path)
        {
            var projectPath = GetProjectPath(path);
            var csproj = Load(projectPath);
            var projectGroups = csproj.Descendants(msbuild + "ProjectGroup");
            return "";
        }

        public string GetProjectPath(string path, int depth = 0)
        {
            var directories = GetDirectoryName(path).Split(DirectorySeparatorChar);
            var newDirectories = directories.Take(directories.Length - depth).ToArray();
            var final = Join(DirectorySeparatorChar.ToString(), newDirectories);
            var projectFiles = GetFiles(final, "*.csproj");
            if (projectFiles.FirstOrDefault() != null)
                return projectFiles.First();
            depth = depth + 1;
            return GetProjectPath(path, depth);
        }
    }
}
