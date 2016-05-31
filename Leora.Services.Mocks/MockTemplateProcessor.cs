using Leora.Services.Contracts;
using System.Collections.Generic;
using System;

namespace Leora.Services.Mocks
{
    public class MockTemplateProcessor : IDotNetTemplateProcessor, ITemplateProcessor
    {
        public string[] ProcessTemplate(string[] template, string name)
        {
            return new List<string>().ToArray();
        }

        public string[] ProcessTemplate(string[] template, string name, string namespacename, string rootNamespace)
        {
            return new List<string>().ToArray();
        }
    }
}
