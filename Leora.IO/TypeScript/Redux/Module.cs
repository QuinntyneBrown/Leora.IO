using Leora.IO.Configuration;
using Leora.IO.Data.Contracts;
using Leora.IO.ExtensionMethods;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace Leora.IO.TypeScript.Redux
{
    public static class Module
    {
        static Module()
        {
            templateRepository = UnityConfiguration.GetContainer().Resolve<ITemplateRepository>();
        }

        private static ITemplateRepository templateRepository;

        public static string[] Get(dynamic options)
        {
            string entityNameSnakeCase = options.entityNameSnakeCase;
            string entityNamePascalCase = entityNameSnakeCase.SnakeCaseToPascalCase();
            string entityNameCamelCase = entityNamePascalCase.CamelCase();
            var lines = new List<string>();

            var index = 0;
            foreach (var line in templateRepository.GetByNameLanguageFramework(GetModuleTemplate(options), "TypeScript", "Redux").Lines)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", entityNamePascalCase);
                newline = newline.Replace("{{ entityNameCamelCase }}", entityNameCamelCase);
                newline = newline.Replace("{{ entityNameSnakeCase }}", entityNameSnakeCase);
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }

        public static string GetModuleTemplate(dynamic options)
        {
            string module = "module";
            try
            {
                if (options.crud == null)
                {
                    module = "module-crud";
                }
            }
            catch
            {
                
            }

            return module;
        }
    }
}
