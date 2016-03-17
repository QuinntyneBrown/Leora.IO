using Leora.IO.Configuration;
using Leora.IO.Data.Contracts;
using Leora.IO.ExtensionMethods;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace Leora.IO.CSharp.WebAPI
{
    public class Service
    {
        static Service()
        {
            templateRepository = UnityConfiguration.GetContainer().Resolve<ITemplateRepository>();
        }

        private static ITemplateRepository templateRepository;

        public static string[] Get(string entityNamePascalCase)
        {
            var lines = new List<string>();
            var index = 0;
            foreach (var line in templateRepository.GetByNameLanguageFramework("service", "CSharp", "WebAPI").Lines)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", entityNamePascalCase);
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }

        public static string[] GetInterface(string entityNamePascalCase)
        {
            var lines = new List<string>();
            var index = 0;
            foreach (var line in templateRepository.GetByNameLanguageFramework("iservice", "CSharp", "WebAPI").Lines)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", entityNamePascalCase);
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }
    }
}
