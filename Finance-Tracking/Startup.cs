using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Finance_Tracking.Startup))]
namespace Finance_Tracking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
