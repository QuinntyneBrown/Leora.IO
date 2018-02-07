using System;
using Leora.Services.Contracts;
using System.Collections.Generic;
using Leora.Models;

namespace Leora.Services
{
    public class DotNetTemplateProcessor : IDotNetTemplateProcessor
    {
        private readonly INamingConventionConverter _namingConventionConverter;

        public DotNetTemplateProcessor(INamingConventionConverter namingConventionConverter)
        {
            _namingConventionConverter = namingConventionConverter;
        }

        public string[] ProcessTemplate(string[] template, string name, string namespacename)
            => ProcessTemplate(template, name, namespacename, null);

        public string[] ProcessTemplate(string[] template, string name, string namespacename, string rootNamespace)
        {
            var lines = new List<string>();
            var index = 0;
            foreach (var line in template)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", _namingConventionConverter.Convert(NamingConvention.PascalCase, name));
                newline = newline.Replace("{{ entityNameCamelCase }}", _namingConventionConverter.Convert(NamingConvention.CamelCase, name));
                newline = newline.Replace("{{ entityNameSnakeCase }}", _namingConventionConverter.Convert(NamingConvention.SnakeCase, name));
                newline = newline.Replace("{{ entityNameTitleCase }}", _namingConventionConverter.Convert(NamingConvention.TitleCase, name));
                newline = newline.Replace("{{ entityNameAllCaps }}", _namingConventionConverter.Convert(NamingConvention.AllCaps, name));
                newline = newline.Replace("{{ entityNameLowerCase }}", _namingConventionConverter.Convert(NamingConvention.CamelCase, name).ToLower());
                newline = newline.Replace("{{ namespacename }}", _namingConventionConverter.Convert(NamingConvention.PascalCase, namespacename));

                if(!string.IsNullOrEmpty(rootNamespace))
                    newline = newline.Replace("{{ rootNamespacename }}", _namingConventionConverter.Convert(NamingConvention.PascalCase, rootNamespace));

                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }

        public string[] ProcessTemplate(string[] template, string entityName, string name, string namespacename, string rootNamespace)
        {
            Console.WriteLine(name);

            var lines = new List<string>();
            var index = 0;
            foreach (var line in template)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", _namingConventionConverter.Convert(NamingConvention.PascalCase, entityName));
                newline = newline.Replace("{{ entityNameCamelCase }}", _namingConventionConverter.Convert(NamingConvention.CamelCase, entityName));
                newline = newline.Replace("{{ entityNameSnakeCase }}", _namingConventionConverter.Convert(NamingConvention.SnakeCase, entityName));
                newline = newline.Replace("{{ entityNameTitleCase }}", _namingConventionConverter.Convert(NamingConvention.TitleCase, entityName));
                newline = newline.Replace("{{ namePascalCase }}", _namingConventionConverter.Convert(NamingConvention.PascalCase, name));
                newline = newline.Replace("{{ entityNameAllCaps }}", _namingConventionConverter.Convert(NamingConvention.AllCaps, name));
                newline = newline.Replace("{{ namespacename }}", _namingConventionConverter.Convert(NamingConvention.PascalCase, namespacename));

                if (!string.IsNullOrEmpty(rootNamespace))
                    newline = newline.Replace("{{ rootNamespacename }}", _namingConventionConverter.Convert(NamingConvention.PascalCase, rootNamespace));

                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }
    }
}
