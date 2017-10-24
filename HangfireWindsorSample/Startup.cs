using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HangfireWindsorSample.Startup))]
namespace HangfireWindsorSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureHangfire(app);
        }
    }
}
