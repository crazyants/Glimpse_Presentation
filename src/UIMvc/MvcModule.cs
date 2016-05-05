using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using JetBrains.Annotations;
using Kiehl.App.UIMvc.Filters;

namespace Kiehl.App.UIMvc
{
    [UsedImplicitly]
    public class MvcModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // STANDARD MVC SETUP:
            var assembly = typeof(MvcApplication).Assembly;

            builder.RegisterControllers(assembly)
                .PropertiesAutowired();
            builder.RegisterModelBinders(assembly)
                .PropertiesAutowired();
            builder.RegisterFilterProvider();
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();

            //global filters
            builder.RegisterType<HandleErrorAttribute>()
                .AsExceptionFilterFor<Controller>();

            builder.RegisterType<Transaction>()
                .AsActionFilterFor<Controller>();

            //environment services
            builder.RegisterType<EnvironmentMonitor>()
                .AsImplementedInterfaces()
                .SingleInstance();

        }
    }
}