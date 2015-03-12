using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RM.Web.Startup))]
namespace RM.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
