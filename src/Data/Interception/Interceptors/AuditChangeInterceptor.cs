using System;
using System.Data.Entity.Infrastructure;
using Autofac.Extras.NLog;

namespace Kiehl.App.Data.Interception.Interceptors
{
    public class AuditChangeInterceptor : ChangeInterceptor<IAmAuditable>
    {
        public ILogger Logger { get; set; }

        protected override void OnBeforeInsert(DbEntityEntry entry, IAmAuditable item, InterceptionContext context)
        {
            Logger.Trace("OnBeforeInsert");

            item.Created = item.Updated = DateTime.UtcNow;

            base.OnBeforeInsert(entry, item, context);
        }

        protected override void OnBeforeUpdate(DbEntityEntry entry, IAmAuditable item, InterceptionContext context)
        {
            Logger.Trace("OnBeforeInsert");

            item.Updated = DateTime.UtcNow;

            base.OnBeforeUpdate(entry, item, context);
        }
    }
}