using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Kiehl.App.Data.Interception
{
    public abstract class TypeInterceptor : IInterceptor
    {
        private readonly System.Type _targetType;
        public Type TargetType { get { return _targetType; } }

        protected TypeInterceptor(System.Type targetType)
        {
            this._targetType = targetType;
        }

        protected virtual bool IsTargetEntity(DbEntityEntry item, EntityState state)
        {
            //
            // can't use the state from DbEntityEntry because if it is after a delete
            // the state will be set to Detached.
            //
            return state != EntityState.Detached &&
                   this.TargetType.IsInstanceOfType(item.Entity);
        }

        public void Before(InterceptionContext context)
        {
            var states = new[] { EntityState.Added, EntityState.Modified, EntityState.Deleted, };
            foreach (var state in states)
            {
                var entries = context.EntriesByState[state];
                foreach (var entry in entries)
                {
                    if (IsTargetEntity(entry, state))
                    {
                        OnBefore(entry, state, context);
                    }
                }
            }

        }

        public void After(InterceptionContext context)
        {
            var states = new[] { EntityState.Added, EntityState.Modified, EntityState.Deleted, };
            foreach (var state in states)
            {
                var entries = context.EntriesByState[state];
                foreach (var entry in entries)
                {
                    if (IsTargetEntity(entry, state))
                    {
                        OnAfter(entry, state, context);
                    }
                }
            }
        }

        protected virtual void OnBefore(DbEntityEntry item, EntityState state, InterceptionContext context)
        { }
        protected virtual void OnAfter(DbEntityEntry item, EntityState state, InterceptionContext context)
        { }
    }
}