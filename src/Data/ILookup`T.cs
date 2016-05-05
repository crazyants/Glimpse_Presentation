using System.Collections.Generic;
using System.Linq;

namespace Kiehl.App.Data
{
    public interface ILookup<T> where T : class, IAmLookupItem
    {
        IEnumerable<T> All { get; }
    }

    public class Lookup<T> : ILookup<T> where T : class, IAmLookupItem
    {
        private const int MaxItems = 100;

        private readonly ApplicationDbContext context;

        public Lookup(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> All => context.Set<T>()
            .Take(MaxItems)
            .ToArray();
    }
}
