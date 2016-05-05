using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Kiehl.App.Data.Migrations;

namespace Kiehl.App.Data.Interception
{
    public class InterceptingApplicationDbContext : TransactionCoordinatorDbContext
    {
        //use of lazy here allows interceptors to have a dependency on dbcontext
        // and not cause a circular dependency exception.
        private readonly Lazy<IEnumerable<IInterceptor>> interceptors;

        public InterceptingApplicationDbContext(Lazy<IEnumerable<IInterceptor>> interceptors)
        {
            this.interceptors = interceptors;
            Database.SetInitializer((IDatabaseInitializer<InterceptingApplicationDbContext>)new SeedDataInitializer());
        }

        public InterceptingApplicationDbContext(Lazy<IEnumerable<IInterceptor>> interceptors, DbConnection connection) : base(connection)
        {
            this.interceptors = interceptors;
            Database.SetInitializer((IDatabaseInitializer<InterceptingApplicationDbContext>)new SeedDataInitializer());
        }

        private ObjectContext ObjectContext => ((IObjectContextAdapter)this).ObjectContext;

        private ObjectStateManager ObjectStateManager => ObjectContext.ObjectStateManager;

        public override int SaveChanges()
        {
            var intercept = BuildContext();

            intercept.Before();
            var result = base.SaveChanges();
            intercept.After();

            return result;
        }

        public override async Task<int> SaveChangesAsync()
        {
            var intercept = BuildContext();

            intercept.Before();
            var result = await base.SaveChangesAsync();
            intercept.After();

            return result;
        }

        private InterceptionContext BuildContext()
        {
            var entries = ChangeTracker.Entries().ToList();
            var entriesByState = entries.ToLookup(row => row.State);
            return new InterceptionContext(interceptors.Value)
            {
                DbContext = this,
                ObjectContext = ObjectContext,
                ObjectStateManager = ObjectStateManager,
                ChangeTracker = ChangeTracker,
                Entries = entries,
                EntriesByState = entriesByState,
            };
        }
    }
}