using Microsoft.Owin;
using Owin;
using Microsoft.Extensions.DependencyInjection;

[assembly: OwinStartupAttribute(typeof(chat_signalr.Startup))]
namespace chat_signalr
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            app.MapSignalR();
            ConfigureAuth(app.MapSignalR());
            
        }
       
    }
}
