using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjManagement.Startup))]

namespace ProjManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}