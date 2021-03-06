﻿using Leora.Services.Contracts;
using System.Collections.Generic;
using Leora.Models;
using System;

namespace Leora.Services.Mocks
{
    public class MockTemplateManager : ITemplateManager
    {
        public string[] Get(string name, string framework = null)
        {
            return new List<string>().ToArray();
        }

        public string[] Get(FileType fileType, string name, string framework = null)
        {
            return new List<string>().ToArray();
        }

        public string[] Get(FileType fileType, string name, string section, string entityName, string framework = null, string[] sufixList = null)
        {
            return new List<string>().ToArray();
        }

        public string[] Get(FileType fileType, string namespacename, string name, string section, string entityName, string framework = null, string[] sufixList = null)
        {
            throw new NotImplementedException();
        }
    }
}
