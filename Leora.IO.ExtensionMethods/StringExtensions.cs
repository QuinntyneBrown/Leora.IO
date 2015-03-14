﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leora.IO.ExtensionMethods
{
    public static class StringExtensions
    {

        public static Boolean IsDirectory(this string input)
        {
            FileAttributes attr = File.GetAttributes(input);

            return ((attr & FileAttributes.Directory) == FileAttributes.Directory);
        }

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

        public static bool IsAppJavaScriptSpecFile(this string fullPath)
        {
            return (System.IO.Path.GetExtension(fullPath) == ".js"
                    && fullPath.Split(System.IO.Path.DirectorySeparatorChar).Where(x => x == "app").FirstOrDefault() != null
                    && fullPath.Contains("Spec.js") == true
                );
        }

        public static bool IsAppLessFile(this string fullPath)
        {
            return (System.IO.Path.GetExtension(fullPath) == ".less"
                    && fullPath.Split(System.IO.Path.DirectorySeparatorChar).Where(x => x == "app").FirstOrDefault() != null);
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

            var index = 0;

            string currentDirectory = "";

            try
            {




                for (var i = 0; i < directories.Length; i++)
                {
                    currentDirectory = string.Format(@"{0}{1}\", currentDirectory, directories[i]);

                    if (index > 1)
                    {
                        foreach (var file in Directory.GetFiles(currentDirectory))
                        {

                            if (Path.GetExtension(file) == ".csproj")
                            {
                                return currentDirectory;
                            }
                        }
                    }

                    index++;
                }
            }
            catch (Exception exception)
            {
                
            }

            return currentDirectory;
        }

        public static string GetGulpFile(this string fullPath)
        {
            return fullPath.GetProjectDirectory() + @"gulpfile.js";
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

        public static string GetModelsDirectory(this string input)
        {
            return input.GetProjectDirectory() + @"Server/Models";
        }

        public static string GetServerServicesDirectory(this string input)
        {
            return input.GetProjectDirectory() + @"Server/Services";
        }

        public static string GetServerDirectory(this string input)
        {
            return input.GetProjectDirectory() + @"Server";
        }

        public static string GetServerDataDirectory(this string input)
        {
            return input.GetProjectDirectory() + @"Server/Data";
        }

        public static string GetServerDataContractsDirectory(this string input)
        {
            return input.GetProjectDirectory() + @"Server/Data/Contracts";
        }
    }
}
