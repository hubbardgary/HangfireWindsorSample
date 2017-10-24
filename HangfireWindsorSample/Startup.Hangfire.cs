using Hangfire;
using Hangfire.MemoryStorage;
using Owin;

namespace HangfireWindsorSample
{
    public partial class Startup
    {
        private void ConfigureHangfire(IAppBuilder app)
        {
            //GlobalConfiguration.Configuration.UseSqlServerStorage(@"Data Source=(LocalDB)\MSSQLLocalDB;Database=HangfireSample");
            GlobalConfiguration.Configuration.UseMemoryStorage();

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}