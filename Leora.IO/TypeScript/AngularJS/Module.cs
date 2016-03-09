using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leora.IO.Configuration;
using Leora.IO.ExtensionMethods;
using Leora.IO.Data.Contracts;
using Microsoft.Practices.Unity;

namespace Leora.IO.TypeScript.AngularJS
{
    public static class Module
    {
        static Module()
        {
            templateRepository = UnityConfiguration.GetContainer().Resolve<ITemplateRepository>();
        }

        public static string[] Get(string moduleName, bool includeCore)
        {
            var lines = new List<string>();
            var index = 0;

            foreach (var line in templateRepository.GetByNameLanguageFramework("module", "TypeScript", "AngularJS").Lines)
            {
                var newline = line.Replace("{{ moduleNameCamelCase }}", moduleName.CamelCase());
                newline = newline.Replace("{{ moduleName }}", moduleName.FirstCharToUpper());

                if (includeCore)
                {
                    lines.Add(newline);    
                }
                else if (!(index >= 3 && index <= 6))
                {
                    lines.Add(newline);
                }
 
                
                index++;
            }
            return lines.ToArray();            
        }

        private static ITemplateRepository templateRepository { get; set; }
    }
}
