using Leora.Models;
using Leora.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;

namespace Leora.Services
{
    public class TemplateManager : ITemplateManager
    {
        public string[] Get(string name, string framework = null)
        {
            List<string> lines = new List<string>();
            string templateName = $"Leora.Templates.{framework}.{name}.txt";

            try
            {
                using (System.IO.Stream stream = typeof(Leora.Templates.Infrastructure.Constants).Assembly.GetManifestResourceStream(templateName))
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            lines.Add(line);
                        }
                    }
                    return lines.ToArray();
                }
            } catch(Exception exception)
            {
                Console.WriteLine("Error:" + templateName);
                
            }
            return lines.ToArray();
        }

        public string[] Get(FileType fileType, string name, string framework = null)
        {
            List<string> lines = new List<string>();
            string templateName = $"Leora.Templates.{framework}.{name}.{GetFileTypeExtension(fileType)}.txt";
                 
            using (System.IO.Stream stream = typeof(Leora.Templates.Infrastructure.Constants).Assembly.GetManifestResourceStream(templateName))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
                return lines.ToArray();
            }
        }

        public string[] Get(FileType fileType, string name, string section, string entityName, string framework = null)
        {
            List<string> lines = new List<string>();
            string templateName = $"Leora.Templates.{framework}.{name}.{GetFileTypeExtension(fileType)}.txt";
            string templateFullName = $"Leora.Templates.{framework}.{section}.{framework}{entityName}.{GetFileTypeExtension(fileType)}.txt";
            Stream stream = null;
            stream = typeof(Leora.Templates.Infrastructure.Constants).Assembly.GetManifestResourceStream(templateFullName);
            
            if (stream == null)
                stream = typeof(Leora.Templates.Infrastructure.Constants).Assembly.GetManifestResourceStream(templateName);            

            using (stream)
            {
                using (var streamReader = new StreamReader(stream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
                return lines.ToArray();
            }
        }

        public string GetFileTypeExtension(FileType fileType)
        {
            switch (fileType)
            {
                case FileType.Css:
                    return "css";
                case FileType.TypeScript:
                    return "typescript";
                case FileType.Html:
                    return "html";
                case FileType.CSharp:
                    return "csharp";
                case FileType.Scss:
                    return "scss";
                case FileType.MarkDown:
                    return "md";
                case FileType.Json:
                    return "json";
                case FileType.JavaScript:
                    return "js";
            }
            throw new NotImplementedException();
        }
    }
}
