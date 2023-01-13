using Microsoft.Owin;
using Owin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;


[assembly: OwinStartupAttribute(typeof(chat_signalr.Startup))]
namespace chat_signalr
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            
           // ConfigureAuth(app);
            app.MapSignalR();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login")
            });
            app.MapSignalR();
        }
       
    }
}
