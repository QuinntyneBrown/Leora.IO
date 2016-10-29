using System;
using Leora.Services.Contracts;
using System.Collections.Generic;
using Leora.Models;

namespace Leora.Services
{
    public class CustomElementsTemplateProcessor : ICustomElementsTemplateProcessor
    {
        private readonly INamingConventionConverter _namingConventionConverter;

        public CustomElementsTemplateProcessor(INamingConventionConverter namingConventionConverter)
        {
            _namingConventionConverter = namingConventionConverter;
        }

        public string[] ProcessTemplate(string[] template, string name, string prefix)
        {
            var lines = new List<string>();
            var index = 0;

            foreach (var line in template)
            {
                string newline = line;
                if (!string.IsNullOrEmpty(name))
                {
                    newline = line.Replace("{{ prefix }}", _namingConventionConverter.Convert(NamingConvention.SnakeCase, prefix));
                    newline = newline.Replace("{{ entityNamePascalCase }}", _namingConventionConverter.Convert(NamingConvention.PascalCase, name));
                    newline = newline.Replace("{{ entityNameCamelCase }}", _namingConventionConverter.Convert(NamingConvention.CamelCase, name));
                    newline = newline.Replace("{{ entityNameSnakeCase }}", _namingConventionConverter.Convert(NamingConvention.SnakeCase, name));
                    newline = newline.Replace("{{ entityNameTitleCase }}", _namingConventionConverter.Convert(NamingConvention.TitleCase, name));
                    newline = newline.Replace("{{ entityNameAllCaps }}", _namingConventionConverter.Convert(NamingConvention.AllCaps, name));
                    newline = newline.Replace("{{ entityNameLowerCase }}", _namingConventionConverter.Convert(NamingConvention.CamelCase, name).ToLower());
                }
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();

        }
    }
}
