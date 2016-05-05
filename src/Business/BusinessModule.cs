﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Features.Variance;
using Kiehl.App.Business.Mediation;
using MediatR;
using Module = Autofac.Module;

namespace Kiehl.App.Business
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //TODO: clean this up scanning the assembly multiple times is prolly not a good idea.

            builder.RegisterSource(new ContravariantRegistrationSource());

            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .As(type => type.GetInterfaces()
                    .Where(interfacetype => interfacetype.IsClosedTypeOf(typeof(ISearch<>))));


            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>) c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

            //register all notification handlers
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .As(type => type.GetInterfaces()
                    .Where(interfacetype => interfacetype.IsClosedTypeOf(typeof(IAsyncNotificationHandler<>))));

            //register all pre handlers
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .As(type => type.GetInterfaces()
                    .Where(interfacetype => interfacetype.IsClosedTypeOf(typeof(IAsyncPreRequestHandler<>))));

            //register all post handlers
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .As(type => type.GetInterfaces()
                    .Where(interfacetype => interfacetype.IsClosedTypeOf(typeof(IAsyncPostRequestHandler<,>))));


            //register all handlers
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .As(type => type.GetInterfaces()
                    .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IAsyncRequestHandler<,>)))
                    .Select(interfaceType => new KeyedService("asyncRequestHandler", interfaceType)));


            //register pipeline decorators
            builder.RegisterGenericDecorator(typeof(AsyncMediatorPipeline<,>), typeof(IAsyncRequestHandler<,>),
                "asyncRequestHandler");

        }
    }
}