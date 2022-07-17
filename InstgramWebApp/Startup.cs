using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InstgramWebApp.Startup))]
namespace InstgramWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
