using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Leora.Services.Contracts;
using System.Xml.Linq;
using System.Linq;
using Leora.Models;
using System.Collections.Generic;
using static System.String;
using static System.IO.Directory;
using static System.IO.Path;
using System.IO;
using System.Reflection;

namespace Leora.Services.Tests
{
    [TestClass]
    public class ProjectManagerTests
    {
        ProjectManager _projectManager;
        protected readonly XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";

        [TestInitialize]
        public void Setup()
        {
            _projectManager = new ProjectManager(null);

        }

        [TestMethod]
        public void TestAddMethod()
        {
            _projectManager = new ProjectManager(null);
            var csproj = XDocument.Parse(Get("empty-project-file.txt"));
            var result = _projectManager.Add(csproj, "/my-file.ts", FileType.TypeScript);
            var typeScriptCompileNodes = result.Descendants(msbuild + "ItemGroup").FirstOrDefault(x => x.Descendants(msbuild + "TypeScriptCompile").Any());
            var name = ((System.Xml.Linq.XElement)typeScriptCompileNodes.FirstNode).Name;
            Assert.AreEqual(name.ToString(), msbuild + "TypeScriptCompile");
        }


        [TestMethod]
        public void TestAddItemGroupMethod()
        {
            _projectManager = new ProjectManager(null);
            var csproj = XDocument.Parse(Get("empty-project-file.txt"));
            var originalCount = csproj.Descendants(msbuild + "ItemGroup").Count();
            var result = _projectManager.AddItemGroup(FileType.TypeScript, csproj);
            Assert.AreEqual(originalCount + 1, csproj.Descendants(msbuild + "ItemGroup").Count());
        }

        [TestMethod]
        public void TestAddTypeSciptFlagsMethod()
        {
            _projectManager = new ProjectManager(null);
            var csproj = XDocument.Parse(Get("empty-project-file.txt"));
            var result = _projectManager.AddTypeSciptFlags(csproj);
        }

        [TestMethod]
        public void TestProcessMethod()
        {

        }

        [TestMethod]
        public void TestGetRelativePathAndProjFileMethod()
        {

        }

        [TestMethod]
        public void TestGetItemGroupMethod()
        {

        }

        public string Get(string name)
        {
            List<string> lines = new List<string>();

            using (System.IO.Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"Leora.Services.Tests.{name}"))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
                return Join("",lines);
            }
        }

    }
}
