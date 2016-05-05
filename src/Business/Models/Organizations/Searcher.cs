using System;
using System.Linq;
using System.Linq.Expressions;
using Kiehl.App.Data;
using Kiehl.App.Data.Models;

namespace Kiehl.App.Business.Models.Organizations
{
    public class OrganizationsSearcher : ISearch<Organization>
    {
        private readonly ApplicationDbContext context;

        public OrganizationsSearcher(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Organization> All()
        {
            return context.Organizations;
        }

        public IQueryable<Organization> GetById(int id)
        {
            return context.Organizations
                .Where(FilterById(id));
        }

        public IQueryable<Organization> GetBySearch(SearchPager searchPager)
        {
            throw new NotImplementedException();
        }

        private Expression<Func<Organization, bool>> FilterById(int id)
        {
            return organization => organization.Id == id;
        }
    }
}
