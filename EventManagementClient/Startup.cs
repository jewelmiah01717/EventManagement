using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventManagementClient.Startup))]
namespace EventManagementClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
