using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;

namespace Kiehl.App.UIMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private IContainer container;
        private IEnvironmentMonitor monitor { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ContainerConfig.RegisterContainer(cntr =>
            {
                container = cntr;
                monitor = cntr.ResolveOptional<IEnvironmentMonitor>();
            });
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            monitor?.AppStarted();
        }

        protected void Application_End()
        {
            monitor?.AppEnded();
        }
    }
}
