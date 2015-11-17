using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Config
{
    public class CommonConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("internalSecret")]
        public string InternalSecret
        {
            get { return (string)this["internalSecret"]; }
            set { this["internalSecret"] = value; }
        }

        [ConfigurationProperty("jwtKey")]
        public string JwtKey
        {
            get { return (string)this["jwtKey"]; }
            set { this["jwtKey"] = value; }
        }

        [ConfigurationProperty("jwtIssuer")]
        public string JwtIssuer
        {
            get { return (string)this["jwtIssuer"]; }
            set { this["jwtIssuer"] = value; }
        }

        [ConfigurationProperty("jwtAudience")]
        public string JwtAudience
        {
            get { return (string)this["jwtAudience"]; }
            set { this["jwtAudience"] = value; }
        }

        public static CommonConfiguration Config
        {
            get { return ConfigurationManager.GetSection("commonConfiguration") as CommonConfiguration; }
        }
    }
}
