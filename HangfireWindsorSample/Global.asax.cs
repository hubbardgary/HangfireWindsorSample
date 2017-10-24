using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Hangfire;
using Hangfire.Windsor;
using HangfireWindsorSample.Services;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HangfireWindsorSample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Web API routes must be configured before MVC routes
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureWindsor();
        }

        private static void ConfigureWindsor()
        {
            var container = new WindsorContainer().Install(FromAssembly.This());
            container.Register(Component.For<IHelperService>().ImplementedBy<HelperService>());
            container.Register(Component.For<ITestService>().ImplementedBy<TestService>());

            // Register controllers
            container.Register(Classes.FromThisAssembly()
                .Pick().If(t => t.Name.EndsWith("Controller"))
                .Configure(configurer => configurer.Named(configurer.Implementation.Name))
                .LifestylePerWebRequest());

            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            // Tell Hangfire to use the dependency graph we defined for Windsor
            JobActivator.Current = new WindsorJobActivator(container.Kernel);
        }
    }
}
