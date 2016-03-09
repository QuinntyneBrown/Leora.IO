using Leora.IO.Configuration;
using Leora.IO.Data.Contracts;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using Leora.IO.ExtensionMethods;

namespace Leora.IO.TypeScript.Redux
{
    public static class Reducers
    {
        static Reducers()
        {
            templateRepository = UnityConfiguration.GetContainer().Resolve<ITemplateRepository>();
        }

        public static string[] Get(string entityNameSnakeCase)
        {
            var entityNamePascalCase = entityNameSnakeCase.SnakeCaseToPascalCase();
            var entityNameCamelCase = entityNamePascalCase.CamelCase();
            var lines = new List<string>();
            var index = 0;
            foreach (var line in templateRepository.GetByNameLanguageFramework("reducers", "TypeScript", "Redux").Lines)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", entityNamePascalCase);
                newline = newline.Replace("{{ entityNameCamelCase }}", entityNameCamelCase);
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }

        private static ITemplateRepository templateRepository;
    }
}
