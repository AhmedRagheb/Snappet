using Microsoft.Owin;
using Owin;
using Snappet.Business.Injection;

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
