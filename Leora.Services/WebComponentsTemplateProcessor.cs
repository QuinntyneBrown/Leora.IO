using Leora.Services.Contracts;
using System.Collections.Generic;
using Leora.Models;

namespace Leora.Services
{
    public class WebComponentsTemplateProcessor : IWebComponentsTemplateProcessor
    {
        private readonly INamingConventionConverter _namingConventionConverter;

        public WebComponentsTemplateProcessor(INamingConventionConverter namingConventionConverter)
        {
            _namingConventionConverter = namingConventionConverter;
        }

        public string[] ProcessTemplate(string[] template, string name)
        {
            var lines = new List<string>();
            var index = 0;
            foreach (var line in template)
            {
                var newline = line.Replace("{{ name }}", _namingConventionConverter.Convert(NamingConvention.PascalCase, name));
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }
    }
}
