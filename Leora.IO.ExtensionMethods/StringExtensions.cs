using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.IO.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string CamelCase(this string input)
        {
            return input.First().ToString().ToLower() + input.Substring(1); 
        }

        public static string FirstCharToUpper(this string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static string GetModuleName(this string fullPath)
        {
            var directories = fullPath.Split(System.IO.Path.DirectorySeparatorChar);

            for (var i = 0; i < directories.Length; i++)
            {
                if (directories[i] == "app")
                {
                    return directories[i + 1];
                }
            }

            throw new InvalidOperationException();
        }

        public static string GetProjectName(this string fullPath)
        {
            var directories = fullPath.Split(System.IO.Path.DirectorySeparatorChar);

            for (var i = 0; i < directories.Length; i++)
            {
                if (directories[i] == "app")
                {
                    return directories[i - 1];
                }
            }

            throw new InvalidOperationException();
        }

        public static bool IsAppJavaScriptFile(this string fullPath)
        {
            return (System.IO.Path.GetExtension(fullPath) == ".js"
                    && fullPath.Split(System.IO.Path.DirectorySeparatorChar).Where(x => x == "app").FirstOrDefault() != null
                    && fullPath.Contains("Spec.js") == false
                );
        }

        public static bool IsInsideAppFolder(this string fullPath)
        {

            return (System.IO.Directory.GetParent(fullPath).Name == "app");
        }

        public static bool IsInsideComponentsFolder(this string fullPath)
        {
            return (Directory.GetParent(fullPath).Name == "components");
        }

        public static string GetAppDirectory(this string fullPath)
        {
            var directories = fullPath.Split(System.IO.Path.DirectorySeparatorChar);

            string appDirectory = "";

            for (var i = 0; i < directories.Length; i++)
            {
                appDirectory = string.Format(@"{0}{1}\", appDirectory, directories[i]);

                if (directories[i] == "app")
                {
                    return appDirectory;
                }
            }

            return appDirectory;
        }

        public static string GetProjectDirectory(this string fullPath)
        {
            var directories = fullPath.Split(System.IO.Path.DirectorySeparatorChar);

            string projectDirectory = "";

            for (var i = 0; i < directories.Length; i++)
            {
                if (directories[i] == "app")
                {
                    return projectDirectory;
                }

                projectDirectory = string.Format(@"{0}{1}\", projectDirectory, directories[i]);
            }

            return projectDirectory;
        }

        public static string GetGulpFile(this string fullPath)
        {
            return fullPath.GetProjectDirectory() + @"\gulpfile.js";
        }

        public static bool IsProjectAppFile(this string fullPath)
        {
            var appDirectory = GetAppDirectory(fullPath);
            return (fullPath == appDirectory + @"\app.js");
        }

        public static bool IsGenerator(this string fullPath)
        {
            return (fullPath.First().ToString() == "`");

        }

        public static string SnakeCase(this string input)
        {
            return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + x.ToString() : x.ToString())).ToLower();
        }
    }
}
