using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RealManagement.Startup))]
namespace RealManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
