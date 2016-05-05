using Kiehl.App.Data.Models;
using MediatR;

namespace Kiehl.App.Business.Models.Features
{
    public class FeatureDisabledNotification : IAsyncNotification
    {
        public Organization Organization { get; set; }
        public Feature Feature { get; set; }

        public FeatureDisabledNotification(Organization organization, Feature feature)
        {
            Organization = organization;
            Feature = feature;
        }
    }
}
