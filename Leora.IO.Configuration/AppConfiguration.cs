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

        [ConfigurationProperty("gulpEnabled", IsRequired = true)]
        public bool GulpEnabled
        {
            get { return (bool)this["gulpEnabled"]; }
            set { this["gulpEnabled"] = value; }
        }

        [ConfigurationProperty("cachedTemplatesEnabled", IsRequired = true)]
        public bool CachedTemplatesEnabled
        {
            get { return (bool)this["cachedTemplatesEnabled"]; }
            set { this["cachedTemplatesEnabled"] = value; }
        }

        [ConfigurationProperty("autoJasmineSpecsEnabled", IsRequired = true)]
        public bool AutoJasmineSpecsEnabled
        {
            get { return (bool)this["autoJasmineSpecsEnabled"]; }
            set { this["autoJasmineSpecsEnabled"] = value; }
        }

        [ConfigurationProperty("autoResponsiveEnabled", IsRequired = true)]
        public bool AutoResponsiveEnabled
        {
            get { return (bool)this["autoResponsiveEnabled"]; }
            set { this["autoResponsiveEnabled"] = value; }
        }

        public static AppConfiguration Config
        {
            get { return ConfigurationManager.GetSection("appConfiguration") as AppConfiguration; }
        }
    }
}
