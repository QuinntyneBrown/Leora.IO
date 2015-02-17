using System.Collections.Generic;
using System.IO;
using Leora.IO.Data.Contracts;
using Leora.IO.ExtensionMethods;
using Leora.IO.Configuration;
using Microsoft.Practices.Unity;

namespace Leora.IO.TypeScript.AngularJS.Jasmine
{
    public static class ComponentSpec
    {
        static ComponentSpec()
        {
            templateRepository = UnityConfiguration.GetContainer().Resolve<ITemplateRepository>();
        }

        public static string[] Get(string moduleName, string componentName)
        {
            var lines = new List<string>();

            foreach (var line in templateRepository.GetByNameLanguageFramework("componentSpec", "TypeScript", "AngularJS").Lines)
            {
                var newline = line.Replace("{{ moduleNameCamelCase }}", moduleName.CamelCase());
                newline = newline.Replace("{{ moduleName }}", moduleName.FirstCharToUpper());
                newline = newline.Replace("{{ componentNameCamelCase }}", componentName.CamelCase());
                newline = newline.Replace("{{ componentNameSnakeCase }}", componentName.SnakeCase());
                newline = newline.Replace("{{ componentName }}", componentName.FirstCharToUpper());
                lines.Add(newline);
            }

            return lines.ToArray();
        }

        private static ITemplateRepository templateRepository { get; set; }
    }
}
