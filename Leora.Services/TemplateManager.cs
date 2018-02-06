using Leora.Models;
using Leora.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;

namespace Leora.Services
{
    public class TemplateManager : ITemplateManager
    {
        protected readonly INamingConventionConverter _namingConventionConverter;
        public TemplateManager()
        {

        }

        public TemplateManager(INamingConventionConverter namingConventionConverter)
        {
            _namingConventionConverter = namingConventionConverter;
        }

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
            return ConvertFileStreamToStringArray(typeof(Leora.Templates.Infrastructure.Constants).Assembly.GetManifestResourceStream(templateName));
        }

        public string[] Get(FileType fileType, string name, string section, string entityName, string framework=null, string[] sufixList = null)
        {
            sufixList = sufixList == null ? new string[0] : sufixList;
             
            List<string> lines = new List<string>();
            string templateName = $"Leora.Templates.{framework}.{name}.{GetFileTypeExtension(fileType)}.txt";
            string templateFullName = $"Leora.Templates.{framework}.{section}.{framework}{entityName}.{GetFileTypeExtension(fileType)}.txt";
            
            Stream stream = null;
            stream = typeof(Leora.Templates.Infrastructure.Constants).Assembly.GetManifestResourceStream(templateFullName);


            if (stream == null) {
                foreach(var sufix in sufixList)
                {                    
                    if (HasSufix(entityName,sufix)) {                        
                        var sufixPascalCase = _namingConventionConverter.Convert(NamingConvention.PascalCase, sufix);
                        templateFullName = $"Leora.Templates.{framework}.{section}.{framework}{sufixPascalCase}.{GetFileTypeExtension(fileType)}.txt";                        
                        stream = typeof(Leora.Templates.Infrastructure.Constants).Assembly.GetManifestResourceStream(templateFullName);
                        if (stream != null)
                            break;
                    }
                }
            }
            if (stream == null)
                stream = typeof(Leora.Templates.Infrastructure.Constants).Assembly.GetManifestResourceStream(templateName);

            return ConvertFileStreamToStringArray(stream);
        }

        public string[] ConvertFileStreamToStringArray(Stream stream)
        {
            List<string> lines = new List<string>();
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
                case FileType.Config:
                    return "config";
            }
            throw new NotImplementedException();
        }

        public bool HasSufix(string value, string sufix) {
            value = _namingConventionConverter.Convert(NamingConvention.PascalCase, value);
            sufix = _namingConventionConverter.Convert(NamingConvention.PascalCase, sufix);
            return value.EndsWith(sufix);
        }

        public string[] Get(FileType fileType, string namespacename, string name, string section, string entityName, string framework = null, string[] sufixList = null)
        {
            throw new NotImplementedException();
        }
    }
}
