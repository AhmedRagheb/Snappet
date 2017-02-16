using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Snappet.Web.Startup))]
namespace Snappet.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
