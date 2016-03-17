﻿using Leora.IO.Configuration;
using Leora.IO.Data.Contracts;
using Leora.IO.ExtensionMethods;
using Microsoft.Practices.Unity;
using System.Collections.Generic;


namespace Leora.IO.CSharp.WebAPI
{
    public static class Dto
    {
        static Dto()
        {
            templateRepository = UnityConfiguration.GetContainer().Resolve<ITemplateRepository>();
        }

        private static ITemplateRepository templateRepository;

        public static string[] Get(string entityNamePascalCase)
        {
            var lines = new List<string>();
            var index = 0;
            foreach (var line in templateRepository.GetByNameLanguageFramework("dto", "CSharp", "WebAPI").Lines)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", entityNamePascalCase);
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }

        public static string[] GetRequest(string entityNamePascalCase)
        {

            var lines = new List<string>();
            var index = 0;
            foreach (var line in templateRepository.GetByNameLanguageFramework("dtoAddOrUpdateRequest", "CSharp", "WebAPI").Lines)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", entityNamePascalCase);
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }

        public static string[] GetResponse(string entityNamePascalCase)
        {

            var lines = new List<string>();
            var index = 0;
            foreach (var line in templateRepository.GetByNameLanguageFramework("dtoAddOrUpdateResponse", "CSharp", "WebAPI").Lines)
            {
                var newline = line.Replace("{{ entityNamePascalCase }}", entityNamePascalCase);
                lines.Add(newline);
                index++;
            }
            return lines.ToArray();
        }
    }
}
