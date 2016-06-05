using System;
using Leora.Services.Contracts;
using Leora.Models;
using System.Collections.Generic;

namespace Leora.Services
{
    public class MicroserviceTemplateProcessor : IMicroserviceTemplateProcessor
    {
        private readonly INamingConventionConverter _namingConventionConverter;

        public MicroserviceTemplateProcessor(INamingConventionConverter namingConventionConverter)
        {
            _namingConventionConverter = namingConventionConverter;
        }

        public string[] ProcessTemplate(string[] template, string name, string entity)
        {
            var lines = new List<string>();
            var index = 0;
            foreach (var line in template)
            {
                var newline = line.Replace("{{ name }}", _namingConventionConverter.Convert(NamingConvention.PascalCase, name));
                newline = newline.Replace("{{ entity }}", _namingConventionConverter.Convert(NamingConvention.PascalCase, entity));
                newline = newline.Replace("{{ entityNameCamelCase }}", _namingConventionConverter.Convert(NamingConvention.CamelCase, entity));
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }

    }
}
