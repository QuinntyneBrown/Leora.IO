using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leora.IO.Configuration;
using Leora.IO.Data.Contracts;
using Leora.IO.ExtensionMethods;
using Microsoft.Practices.Unity;

namespace Leora.IO.TypeScript.AngularJS
{
    public static class Service
    {
        static Service()
        {
            templateRepository = UnityConfiguration.GetContainer().Resolve<ITemplateRepository>();
        }

        public static string[] Get(string moduleName, string className)
        {
            var lines = new List<string>();

            foreach (var line in templateRepository.GetByNameLanguageFramework("service","TypeScript","AngularJS").Lines)
            {
                var newline = line.Replace("{{ moduleNameCamelCase }}", moduleName.CamelCase());
                newline = newline.Replace("{{ moduleName }}", moduleName.FirstCharToUpper());
                newline = line.Replace("{{ classNameCamelCase }}", className.CamelCase());
                newline = newline.Replace("{{ className }}", className.FirstCharToUpper());
                lines.Add(newline);
            }

            return lines.ToArray();
        }

        private static ITemplateRepository templateRepository;
    }
}
