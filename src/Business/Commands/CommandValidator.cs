using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac.Extras.NLog;
using Kiehl.App.Business.Mediation;
using MediatR;
using NExtensions;

namespace Kiehl.App.Business.Commands
{
    public abstract class CommandValidator<T> : IAsyncPreRequestHandler<T> where T : IAsyncRequest<ICommandResult>
    {
        public ILogger Logger { get; set; }
        public IEnumerable<Action<T>> Validators { get; set; }

        public Task Handle(T request)
        {
            Logger.Trace("Handle");

            Validators.ForEach(v => v(request));

            return Task.FromResult(request);
        }
    }
}