using System.Collections.Generic;
using System.IO;
using Leora.IO.Data.Contracts;
using Leora.IO.Data.FileSystem;
using Leora.IO.ExtensionMethods;
using Leora.IO.Configuration;
using Microsoft.Practices.Unity;

namespace Leora.IO.TypeScript.AngularJS
{
    public static class RouteResolver
    {
        static RouteResolver()
        {
            templateRepository = UnityConfiguration.GetContainer().Resolve<ITemplateRepository>();
        }

        public static string[] Get(string moduleName)
        {
            var lines = new List<string>();

            foreach (var line in templateRepository.GetByNameLanguageFramework("routeResolver", "TypeScript", "AngularJS").Lines)
            {
                var newline = line.Replace("{{ moduleNameCamelCase }}", moduleName.CamelCase());
                newline = newline.Replace("{{ moduleName }}", moduleName.FirstCharToUpper());
                lines.Add(newline);
            }

            return lines.ToArray();
        }

        private static ITemplateRepository templateRepository { get; set; }
    }
}
