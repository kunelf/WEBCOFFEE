using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebSiteCafe.Startup))]
namespace WebSiteCafe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
