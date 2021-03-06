﻿using Leora.Services.Contracts;
using Leora.Models;
using System;
using System.Xml.Linq;

namespace Leora.Services.Mocks
{
    public class MockProjectManager : IProjectManager
    {
        public XDocument Add(XDocument csproj, string relativePath, FileType fileType = FileType.TypeScript, bool trace = false)
        {
            return csproj;
        }

        public RelativePathAndProjFile GetRelativePathAndProjFile(string fullFilePath, int depth = 0, bool trace = false)
        {
            throw new NotImplementedException();
        }

        public void Process(string currentDirectory, string fileName, FileType fileType = FileType.TypeScript, bool trace = false   )
        {
            
        }
    }
}
