using System.Data.Entity.ModelConfiguration;
using Kiehl.App.Data.Models;

namespace Kiehl.App.Data.Overrides
{
    public class OrganizationConfiguration : EntityTypeConfiguration<Organization>
    {
        public OrganizationConfiguration()
        {
            this.HasMany(o => o.Features)
                .WithMany()
                .Map(mo =>
                {
                    mo.MapLeftKey("OrganizationId");
                    mo.MapRightKey("FeatureId");
                    mo.ToTable("OrganizationFeatures");
                });
        }
    }
}
