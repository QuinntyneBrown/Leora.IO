﻿using System;
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
        {
            var lines = new List<string>();
            var index = 0;
            foreach (var line in template)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", _namingConventionConverter.Convert(NamingConvention.PascalCase, name));
                newline = newline.Replace("{{ entityNameCamelCase }}", _namingConventionConverter.Convert(NamingConvention.CamelCase, name));
                newline = newline.Replace("{{ entityNameSnakeCase }}", _namingConventionConverter.Convert(NamingConvention.SnakeCase, name));
                newline = newline.Replace("{{ entityNameTitleCase }}", _namingConventionConverter.Convert(NamingConvention.TitleCasae, name));
                newline = newline.Replace("{{ namespacename }}", _namingConventionConverter.Convert(NamingConvention.PascalCase, namespacename));
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }
    }
}
