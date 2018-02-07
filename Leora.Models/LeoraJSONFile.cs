using System;
namespace Leora.Models
{
    public class LeoraJSONFile
    {
        public bool UseMessaging { get; set; }
        public string ClientNamespace { get; set; }
        public string RootNamespace { get; set; }
        public string Version { get; set; }
        public string Framework { get; set; }
        public bool VanillaSql { get; set; }
    }
}
