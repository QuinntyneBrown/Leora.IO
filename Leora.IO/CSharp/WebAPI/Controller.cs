using Leora.IO.Configuration;
using Leora.IO.Data.Contracts;
using Leora.IO.ExtensionMethods;
using Microsoft.Practices.Unity;
using System.Collections.Generic;


namespace Leora.IO.CSharp.WebAPI
{
    public static class Controller
    {
        static Controller()
        {
            templateRepository = UnityConfiguration.GetContainer().Resolve<ITemplateRepository>();
        }

        private static ITemplateRepository templateRepository { get; set; }
        
        public static string[] Get(string entityNamePascalCase)
        {
            var lines = new List<string>();
            var index = 0;
            foreach (var line in templateRepository.GetByNameLanguageFramework("controller", "CSharp", "WebAPI").Lines)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", entityNamePascalCase);
                newline = newline.Replace("{{ entityNameCamelCase }}", entityNamePascalCase.CamelCase());
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }
    }
}
