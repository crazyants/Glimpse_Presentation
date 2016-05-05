using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Kiehl.App.Data.Interception
{
    public class ChangeInterceptor<T> : TypeInterceptor
    {
        protected override void OnBefore(DbEntityEntry item, EntityState state, InterceptionContext context)
        {
            T tItem = (T)item.Entity;
            switch (state)
            {
                case EntityState.Added:
                    OnBeforeInsert(item, tItem, context);
                    break;
                case EntityState.Deleted:
                    OnBeforeDelete(item, tItem, context);
                    break;
                case EntityState.Modified:
                    OnBeforeUpdate(item, tItem, context);
                    break;
            }
        }

        protected override void OnAfter(DbEntityEntry item, EntityState state, InterceptionContext context)
        {
            T tItem = (T)item.Entity;
            switch (state)
            {
                case EntityState.Added:
                    this.OnAfterInsert(item, tItem, context);
                    break;
                case EntityState.Deleted:
                    this.OnAfterDelete(item, tItem, context);
                    break;
                case EntityState.Modified:
                    this.OnAfterUpdate(item, tItem, context);
                    break;
            }
        }

        protected virtual void OnBeforeInsert(DbEntityEntry entry, T item, InterceptionContext context)
        { }

        protected virtual void OnAfterInsert(DbEntityEntry entry, T item, InterceptionContext context)
        { }

        protected virtual void OnBeforeUpdate(DbEntityEntry entry, T item, InterceptionContext context)
        { }

        protected virtual void OnAfterUpdate(DbEntityEntry entry, T item, InterceptionContext context)
        { }

        protected virtual void OnBeforeDelete(DbEntityEntry entry, T item, InterceptionContext context)
        { }

        protected virtual void OnAfterDelete(DbEntityEntry entry, T item, InterceptionContext context)
        { }

        protected ChangeInterceptor()
            : base(typeof(T))
        {

        }
    }
}