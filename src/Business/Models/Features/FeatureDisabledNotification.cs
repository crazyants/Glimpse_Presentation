using System.Threading.Tasks;
using Autofac.Extras.NLog;
using Kiehl.App.Data;
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

    public class FundsFeatureDisabledNotificationHandler : IAsyncNotificationHandler<FeatureDisabledNotification>
    {
        public ILogger Logger { get; set; }

        public FundsFeatureDisabledNotificationHandler(ApplicationDbContext context)
        {
        }

        public async Task Handle(FeatureDisabledNotification notification)
        {
            Logger.Trace("Handle");

            if (!notification.Feature.Name.Equals(Feature.Funds))
                return;

            await Task.Delay(10000);
        }
    }
}
