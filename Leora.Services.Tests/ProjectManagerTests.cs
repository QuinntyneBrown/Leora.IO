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

        [TestInitialize]
        public void Setup()
        {
            _projectManager = new ProjectManager();
        }

        [TestMethod]
        public void TestAddMethod()
        {
            _projectManager = new ProjectManager();
            var csproj = XDocument.Parse(Get("empty-project-file.txt"));
            var result = _projectManager.Add(csproj, "/", FileType.TypeScript);
            
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
