using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Kiehl.App.Data.Conventions;
using Kiehl.App.Data.Migrations;
using Kiehl.App.Data.Models;
using Kiehl.App.Data.Overrides;

namespace Kiehl.App.Data
{
    public class ApplicationDbContext: DbContext, IUnitOfWork
    {

        public ApplicationDbContext() : base("DefaultConnection")
        {
            //insure default data is setup in the database
            Database.SetInitializer((IDatabaseInitializer<ApplicationDbContext>)new SeedDataInitializer());
            Configuration.LazyLoadingEnabled = true;
        }

        public ApplicationDbContext(DbConnection dbConnection) : base(dbConnection, true)
        {
            //insure default data is setup in the database
            Database.SetInitializer((IDatabaseInitializer<ApplicationDbContext>)new SeedDataInitializer());
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Add(new PrimaryKeyNamingConvention());
            modelBuilder.Conventions.Add(new ForeignKeyNamingConvention());
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new OrganizationConfiguration());
        }

        public virtual IDbSet<Organization> Organizations { get; set; }

        public virtual IDbSet<Feature> Features { get; set; }

        public void BeginTransaction()
        {
            throw new NotImplementedException("Use TransactionCoordinatorDbContext");
        }

        public void CloseTransaction()
        {
            throw new NotImplementedException("Use TransactionCoordinatorDbContext");
        }

        public void CloseTransaction(Exception exception)
        {
            throw new NotImplementedException("Use TransactionCoordinatorDbContext");
        }
    }
}
