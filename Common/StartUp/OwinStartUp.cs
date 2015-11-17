using System;
using System.Threading.Tasks;
using Common.OAuth;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(Common.StartUp.OwinStartUp))]

namespace Common.StartUp
{
    public class OwinStartUp
    {
        public virtual void Configuration(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(new JwtOptions());
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
