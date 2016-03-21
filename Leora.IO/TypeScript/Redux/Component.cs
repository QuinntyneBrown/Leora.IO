using Leora.IO.Configuration;
using Leora.IO.Data.Contracts;
using Leora.IO.ExtensionMethods;
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

        public static string[] Get(dynamic options)
        {
            string entityNameSnakeCase = options.entityNameSnakeCase;
            string entityNamePascalCase = entityNameSnakeCase.SnakeCaseToPascalCase();
            string entityNameCamelCase = entityNamePascalCase.CamelCase();
            string entityNameTitleCase = entityNamePascalCase;
            var lines = new List<string>();
            var index = 0;
            foreach (var line in templateRepository.GetByNameLanguageFramework("component", "TypeScript", "Redux").Lines)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", entityNamePascalCase);
                newline = newline.Replace("{{ entityNameCamelCase }}", entityNameCamelCase);
                newline = newline.Replace("{{ entityNameSnakeCase }}", entityNameSnakeCase);
                newline = newline.Replace("{{ entityNameTitleCase }}", entityNameTitleCase);
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }

        public static string[] List(dynamic options)
        {
            string entityNameSnakeCase = options.entityNameSnakeCase;
            string entityNamePascalCase = entityNameSnakeCase.SnakeCaseToPascalCase();
            string entityNameCamelCase = entityNamePascalCase.CamelCase();
            var lines = new List<string>();
            var index = 0;
            foreach (var line in templateRepository.GetByNameLanguageFramework("list", "TypeScript", "Redux").Lines)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", entityNamePascalCase);
                newline = newline.Replace("{{ entityNameCamelCase }}", entityNameCamelCase);
                newline = newline.Replace("{{ entityNameSnakeCase }}", entityNameSnakeCase);
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }

        public static string[] ListHtml(dynamic options)
        {
            string entityNameSnakeCase = options.entityNameSnakeCase;
            string entityNamePascalCase = entityNameSnakeCase.SnakeCaseToPascalCase();
            string entityNameCamelCase = entityNamePascalCase.CamelCase();
            var lines = new List<string>();
            var index = 0;
            foreach (var line in templateRepository.GetByNameLanguageFramework("list.html", "TypeScript", "Redux").Lines)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", entityNamePascalCase);
                newline = newline.Replace("{{ entityNameCamelCase }}", entityNameCamelCase);
                newline = newline.Replace("{{ entityNameSnakeCase }}", entityNameSnakeCase);
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }

        public static string[] Editor(dynamic options)
        {
            string entityNameSnakeCase = options.entityNameSnakeCase;
            string entityNamePascalCase = entityNameSnakeCase.SnakeCaseToPascalCase();
            string entityNameCamelCase = entityNamePascalCase.CamelCase();
            string entityNameTitleCase = entityNamePascalCase.PascalCaseToTitleCase();
            string entityNameLowerCase = entityNamePascalCase.ToLower();
            var lines = new List<string>();
            var index = 0;
            foreach (var line in templateRepository.GetByNameLanguageFramework("editor", "TypeScript", "Redux").Lines)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", entityNamePascalCase);
                newline = newline.Replace("{{ entityNameCamelCase }}", entityNameCamelCase);
                newline = newline.Replace("{{ entityNameSnakeCase }}", entityNameSnakeCase);
                newline = newline.Replace("{{ entityNameTitleCase }}", entityNameTitleCase);
                newline = newline.Replace("{{ entityNameLowerCase }}", entityNameLowerCase);
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }

        public static string[] EditorHtml(dynamic options)
        {
            string entityNameSnakeCase = options.entityNameSnakeCase;
            string entityNamePascalCase = entityNameSnakeCase.SnakeCaseToPascalCase();
            string entityNameCamelCase = entityNamePascalCase.CamelCase();
            string entityNameTitleCase = entityNamePascalCase.PascalCaseToTitleCase();

            var lines = new List<string>();
            var index = 0;
            foreach (var line in templateRepository.GetByNameLanguageFramework("editor.html", "TypeScript", "Redux").Lines)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", entityNamePascalCase);
                newline = newline.Replace("{{ entityNameCamelCase }}", entityNameCamelCase);
                newline = newline.Replace("{{ entityNameSnakeCase }}", entityNameSnakeCase);
                newline = newline.Replace("{{ entityNameTitleCase }}", entityNameTitleCase);
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }

        public static string[] Container(dynamic options)
        {
            string entityNameSnakeCase = options.entityNameSnakeCase;
            string entityNamePascalCase = entityNameSnakeCase.SnakeCaseToPascalCase();
            string entityNameCamelCase = entityNamePascalCase.CamelCase();
            string entityNameLowerCase = entityNamePascalCase.ToLower();
            var lines = new List<string>();
            var index = 0;
            foreach (var line in templateRepository.GetByNameLanguageFramework("container-crud.ts", "TypeScript", "Redux").Lines)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", entityNamePascalCase);
                newline = newline.Replace("{{ entityNameCamelCase }}", entityNameCamelCase);
                newline = newline.Replace("{{ entityNameSnakeCase }}", entityNameSnakeCase);
                newline = newline.Replace("{{ entityNameLowerCase }}", entityNameLowerCase);
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }

        public static string[] ContainerHtml(dynamic options)
        {
            string entityNameSnakeCase = options.entityNameSnakeCase;
            string entityNamePascalCase = entityNameSnakeCase.SnakeCaseToPascalCase();
            string entityNameCamelCase = entityNamePascalCase.CamelCase();
            var lines = new List<string>();
            var index = 0;
            foreach (var line in templateRepository.GetByNameLanguageFramework("container-crud.html", "TypeScript", "Redux").Lines)
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
