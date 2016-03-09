using Leora.IO.Configuration;
using Leora.IO.Data.Contracts;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace Leora.IO.TypeScript.Redux
{
    public static class Component
    {
        static Component()
        {
            templateRepository = UnityConfiguration.GetContainer().Resolve<ITemplateRepository>();
        }

        private static ITemplateRepository templateRepository;

        public static string[] List(string entityName)
        {
            var lines = new List<string>();
            return lines.ToArray();
        }

        public static string[] Editor()
        {
            var lines = new List<string>();
            return lines.ToArray();
        }
    }
}
