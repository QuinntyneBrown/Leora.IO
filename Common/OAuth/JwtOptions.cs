using Common.Config;
using Microsoft.Owin.Security.Jwt;

namespace Common.OAuth
{
    public class JwtOptions : JwtBearerAuthenticationOptions
    {
        public JwtOptions()
        {
            var config = CommonConfiguration.Config;

            AllowedAudiences = new[] { config.JwtAudience };
            IssuerSecurityTokenProviders = new[] 
            {
                new SymmetricKeyIssuerSecurityTokenProvider(config.JwtIssuer, config.JwtKey)
            };
        }
    }
}
