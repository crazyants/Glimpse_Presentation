using Autofac;
using JetBrains.Annotations;
using Kiehl.App.Data.Interception;
using Kiehl.App.Data.Interception.Interceptors;

namespace Kiehl.App.Data
{
    [UsedImplicitly]
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InterceptingApplicationDbContext>()
                .AsImplementedInterfaces()
                .As<ApplicationDbContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Lookup<>))
                .As(typeof(ILookup<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<AuditChangeInterceptor>()
                .As<IInterceptor>();

        }
    }
}