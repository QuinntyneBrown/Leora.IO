﻿using Leora.IO.Configuration;
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

        public static string[] Get(string entityNameSnakeCase)
        {
            var entityNamePascalCase = entityNameSnakeCase.SnakeCaseToPascalCase();
            var entityNameCamelCase = entityNamePascalCase.CamelCase();
            var lines = new List<string>();
            var index = 0;
            foreach (var line in templateRepository.GetByNameLanguageFramework("module", "TypeScript", "Redux").Lines)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", entityNamePascalCase);
                newline = newline.Replace("{{ entityNameCamelCase }}", entityNameCamelCase);
                newline = newline.Replace("{{ entityNameSnakeCase }}", entityNameSnakeCase);
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }
    }
}
