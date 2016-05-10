using System;
using System.Web.Mvc;
using Autofac;
using Autofac.Extras.NLog;
using Autofac.Integration.Mvc;
using Kiehl.App.Business;
using Kiehl.App.Data;

namespace Kiehl.App.UIMvc
{
    public static class ContainerConfig
    {
        public static void RegisterContainer(Action<IContainer> onReady)
        {
            var builder = new ContainerBuilder();

            //Add application modules, one per assembly
            builder.RegisterModule<DataModule>();
            builder.RegisterModule<BusinessModule>();
            builder.RegisterModule<MvcModule>();

            //Add 3rd party integration modules
            builder.RegisterModule<NLogModule>();
            //needed for action filters, ensures ILogger is resolvable
            builder.RegisterModule<SimpleNLogModule>();

            var container = builder.Build();

            container.ActivateGlimpse();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            onReady(container);
        }
    }
}