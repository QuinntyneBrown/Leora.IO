using System;
using Leora.Services.Contracts;
using System.Collections.Generic;
using Leora.Models;

namespace Leora.Services
{
    public class TemplateProcessor : ITemplateProcessor
    {
        private readonly INamingConventionConverter _namingConventionConverter;

        public TemplateProcessor(INamingConventionConverter namingConventionConverter)
        {
            _namingConventionConverter = namingConventionConverter;
        }

        public string[] ProcessTemplate(string[] template, string name)
        {            
            var lines = new List<string>();
            var index = 0;
            foreach (var line in template)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", _namingConventionConverter.Convert(NamingConvention.PascalCase, name));
                newline = newline.Replace("{{ entityNameCamelCase }}", _namingConventionConverter.Convert(NamingConvention.CamelCase, name));
                newline = newline.Replace("{{ entityNameSnakeCase }}", _namingConventionConverter.Convert(NamingConvention.SnakeCase, name));
                newline = newline.Replace("{{ entityNameTitleCase }}", _namingConventionConverter.Convert(NamingConvention.TitleCase, name));
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
            
        }
    }
}
