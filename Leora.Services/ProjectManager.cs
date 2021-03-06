﻿using System;
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
        private readonly ILeoraJSONFileManager _leoraJSONFileManager;
        protected readonly XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";
        public ProjectManager(ILeoraJSONFileManager leoraJSONFileManager)
        {
            _leoraJSONFileManager = leoraJSONFileManager;
        }
        public void Process(string currentDirectory, string fileName, FileType fileType = FileType.TypeScript, bool trace = false)
        {
            if (_leoraJSONFileManager.GetLeoraJSONFile(currentDirectory, -1).Version.ToLower() == "core")
                return;

            var relativePathAndProjFile = GetRelativePathAndProjFile($"{currentDirectory}//{fileName}", 0,trace);
            if (relativePathAndProjFile != null)
            {
                var csproj = Add(XDocument.Load(relativePathAndProjFile.ProjFile), relativePathAndProjFile.RelativePath, fileType);
                csproj.Save(relativePathAndProjFile.ProjFile);
            }
        }

        public XDocument Add(XDocument csproj, string relativePath, FileType fileType = FileType.TypeScript, bool trace = false)
        {
            if (csproj.ToString().Contains(relativePath))
                return csproj;

            var itemGroup = GetItemGroup(fileType, csproj.Descendants(msbuild + "ItemGroup"));
            if (itemGroup == null)
            {
                AddItemGroup(fileType, csproj);
                itemGroup = csproj.Descendants(msbuild + "ItemGroup").Last();

                if (fileType == FileType.TypeScript)
                {
                    AddTypeSciptFlags(csproj);
                    AddTypeScriptImports(csproj);
                }
            }
            var item = CreateElementByFileType(fileType);
            item.SetAttributeValue("Include", relativePath);                     
            itemGroup.Add(item);
            return csproj;
        }

        public RelativePathAndProjFile GetRelativePathAndProjFile(string fullFilePath, int depth = 0, bool trace = false)
        {

            if(trace)
            {
                Console.WriteLine($"{fullFilePath}:{depth}");
                Console.WriteLine();

            }

            var directories = GetDirectoryName(fullFilePath).Split(DirectorySeparatorChar);

            if (directories.Length < depth)
                return null;

            var newDirectories = directories.Take(directories.Length - depth).ToArray();
            var computedPath = Join(DirectorySeparatorChar.ToString(), newDirectories);
            var projectFiles = GetFiles(computedPath, "*.csproj");

            if (projectFiles.FirstOrDefault() != null)
            {
                var x = directories.Skip(directories.Length - depth).Take(computedPath.Split(DirectorySeparatorChar).Length);

                if(x.Count() == 1)
                    return new RelativePathAndProjFile(GetFileName(fullFilePath), x.First(), projectFiles.First());

                return new RelativePathAndProjFile(GetFileName(fullFilePath),Join(DirectorySeparatorChar.ToString(), x), projectFiles.First());                
            }
            else
            {
                depth = depth + 1;
                return GetRelativePathAndProjFile(fullFilePath, depth, trace);
            }
        }

        public XElement CreateElementByFileType(FileType fileType)
        {
            if(fileType == FileType.TypeScript)
                return new XElement(msbuild + "TypeScriptCompile");

            if (fileType == FileType.Css 
                || fileType == FileType.Html
                || fileType == FileType.Json
                || fileType == FileType.JavaScript)
                return new XElement(msbuild + "Content");

            return new XElement(msbuild + "Compile");
        }

        public XElement GetItemGroup (FileType fileType, IEnumerable<XElement> itemGroups)
        {
            if (fileType == FileType.TypeScript)
                return itemGroups.FirstOrDefault(x => x.Descendants(msbuild + "TypeScriptCompile").Any());

            if (fileType == FileType.Css 
                || fileType == FileType.Html 
                || fileType == FileType.JavaScript 
                || fileType == FileType.Json 
                || fileType == FileType.Config)
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

        public XDocument AddTypeSciptFlags(XDocument csproj)
        {
            var itemGroup = csproj.Descendants(msbuild + "PropertyGroup").Single(x => x.Descendants(msbuild + "ProductVersion").Any());

            if (itemGroup.Descendants(msbuild + "TypeScriptToolsVersion").SingleOrDefault() == null)
                itemGroup.Add(new XElement(msbuild + "TypeScriptToolsVersion") { Value = "2.2" });

            if (itemGroup.Descendants(msbuild + "TypeScriptEmitDecoratorMetadata").SingleOrDefault() == null)
                itemGroup.Add(new XElement(msbuild + "TypeScriptEmitDecoratorMetadata") { Value = "true" });

            if (itemGroup.Descendants(msbuild + "TypeScriptModuleKind").SingleOrDefault() == null)
                itemGroup.Add(new XElement(msbuild + "TypeScriptModuleKind") { Value = "commonjs" });

            if (itemGroup.Descendants(msbuild + "TypeScriptExperimentalDecorators").SingleOrDefault() == null)
                itemGroup.Add(new XElement(msbuild + "TypeScriptExperimentalDecorators") { Value = "True" });
     
            if (itemGroup.Descendants(msbuild + "TypeScritModuleResolution").SingleOrDefault() == null)
                itemGroup.Add(new XElement(msbuild + "TypeScritModuleResolution") { Value = "Classic" });  
                  
            return csproj;
        }

        public XDocument AddTypeScriptImports(XDocument csproj)
        {
            XAttribute projectAttribute = new XAttribute("Project", @"$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v$(VisualStudioVersion)\TypeScript\Microsoft.TypeScript.targets");
            XAttribute conditionAttribute = new XAttribute("Condition", @"Exists('$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v$(VisualStudioVersion)\TypeScript\Microsoft.TypeScript.targets')");
            var xElement = new XElement(msbuild + "Import", projectAttribute, conditionAttribute);
            csproj.Root.Add(xElement);
            return csproj;
        }
    }
}
