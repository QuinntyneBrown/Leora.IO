using System.Configuration;

namespace Leora.IO.Configuration
{
    public class AppConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("projectsFolderPath", IsRequired = true)]
        public string ProjectFolderPath
        {
            get { return (string)this["projectsFolderPath"]; }
            set { this["projectsFolderPath"] = value; }
        }

        [ConfigurationProperty("appFileName", IsRequired = true)]
        public string AppFileName
        {
            get { return (string)this["appFileName"]; }
            set { this["appFileName"] = value; }
        }

        [ConfigurationProperty("gulpFilePath", IsRequired = true)]
        public string GulpFilePath
        {
            get { return (string)this["gulpFilePath"]; }
            set { this["gulpFilePath"] = value; }
        }

        [ConfigurationProperty("templatesPath", IsRequired = true)]
        public string TemplatesPath
        {
            get { return (string)this["templatesPath"]; }
            set { this["templatesPath"] = value; }
        }

        public static AppConfiguration Config
        {
            get { return ConfigurationManager.GetSection("appConfiguration") as AppConfiguration; }
        }
    }
}
