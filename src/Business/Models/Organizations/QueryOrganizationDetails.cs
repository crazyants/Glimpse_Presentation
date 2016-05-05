using System.Data.Entity;
using System.Threading.Tasks;
using Autofac.Extras.NLog;
using Kiehl.App.Data.Models;
using MediatR;

namespace Kiehl.App.Business.Models.Organizations
{
    public class OrganizationDetailsQuery : IAsyncRequest<OrganizationDetails>
    {
        public int OrganizationId { get; set; }

        public OrganizationDetailsQuery(int organizationId)
        {
            OrganizationId = organizationId;
        }
    }

    public class OrganizationDetailsQueryHandler : IAsyncRequestHandler<OrganizationDetailsQuery, OrganizationDetails>
    {
        public ILogger Logger { get; set; }

        private readonly ISearch<Organization> organizations;

        public OrganizationDetailsQueryHandler(ISearch<Organization> organizations)
        {
            this.organizations = organizations;
        }

        public async Task<OrganizationDetails> Handle(OrganizationDetailsQuery query)
        {
            Logger.Trace("Handle::{0}", query.OrganizationId);

            var organization = await organizations.GetById(query.OrganizationId)
                .Include(o => o.Features)
                .Include(o => o.Children)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (organization != null)
            {
                return new OrganizationDetails
                {
                    Organization = organization
                };
            }
            return null;
        }
    }
}
