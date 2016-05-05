using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Kiehl.App.Data.Interception;
using Kiehl.App.Data.Models;
using NExtensions;

namespace Kiehl.App.Data.Migrations
{
    public static class SeedData
    {
        public static void Execute(ApplicationDbContext context)
        {
            AddFeatures(context);

            //populate default entities
            if (!context.Organizations.Any())
            {
                AddDepartment(context);
                AddDivisions(context);
            }
        }

        private static void AddFeatures(ApplicationDbContext context)
        {
            if (context.Features.Any())
                return;

            context.Features.AddOrUpdate(new Feature { Id = 1, Name = Feature.Clients, Description = "Allows Clients to be associated with this Organization." });
            context.Features.AddOrUpdate(new Feature { Id = 2, Name = Feature.Funds, Description = "Creates default Funds for this Organization." });
            context.Features.AddOrUpdate(new Feature { Id = 3, Name = Feature.Payees, Description = "Allows Payees to be associated with this Organization." });
            context.Features.AddOrUpdate(new Feature { Id = 4, Name = Feature.Restitution, Description = "Enables Restitution feature for this Organization." });

            context.SaveChanges();
        }

        private static void AddDepartment(ApplicationDbContext context)
        {
            var department = new Organization
            {
                Id = 1,
                Name = "Kiehl Northwest Global",
                Abbreviation = "KNWG",
                Phone = "1-877-501-2233",
                AddressLine1 = "PO Box 45130",
                City = "Olympia",
                State = "WA",
                PostalCode = "98503-5130",
                FiscalContactSamAccountName = "finnma",
                ITConactSamAccountName = "mongoda",
                GroupName = "Active Directory Group 1",
            };

            AddAudit(department);

            context.Organizations.AddOrUpdate(department);
            context.SaveChanges();
        }

        private static void AddDivisions(ApplicationDbContext context)
        {
            var department = context.Organizations.First(o => o.Abbreviation == "KNWG");
            var divisions = new[]
            {
                new Organization
                {
                    Id = 2,
                    Abbreviation = "kNWT",
                    Name = "Kiehl Northwest Tumwater",
                    GroupName = "Active Directory Group Tumwater",
                    Phone = "360-555-1212",
                    AddressLine1 = "123 Street",
                    City = "Olympia",
                    State = "WA",
                    PostalCode = "98501",
                    FiscalContactSamAccountName = "finnma",
                    ITConactSamAccountName = "johnsb",
                    Parent = department
                },
                new Organization
                {
                    Id = 3,
                    Abbreviation = "KNWL",
                    Name = "Kiehl Northwest Lacey",
                    GroupName = "Active Directory Group Lacey",
                    Phone = "360-555-1212",
                    AddressLine1 = "123 Street",
                    City = "Olympia",
                    State = "WA",
                    PostalCode = "98501",
                    FiscalContactSamAccountName = "finnma",
                    ITConactSamAccountName = "johnsb",
                    Parent = department
                },
                new Organization
                {
                    Id = 3,
                    Abbreviation = "KNWO",
                    Name = "Kiehl Northwest Olympia",
                    GroupName = "Active Directory Group Olympia",
                    Phone = "360-555-1212",
                    AddressLine1 = "123 Street",
                    City = "Olympia",
                    State = "WA",
                    PostalCode = "98501",
                    FiscalContactSamAccountName = "finnma",
                    ITConactSamAccountName = "johnsb",
                    Parent = department
                }
            };

            AddAudit(divisions);

            context.Organizations.AddOrUpdate(divisions);
            context.SaveChanges();

        }

        private static void AddAudit(params IAmAuditable[] auditables)
        {
            var now = DateTime.UtcNow;

            auditables.ForEach(o =>
            {
                o.Created = o.Updated = now;
            });
        }
    }

    public class SeedDataInitializer : IDatabaseInitializer<ApplicationDbContext>, IDatabaseInitializer<InterceptingApplicationDbContext>
    {
        public void InitializeDatabase(ApplicationDbContext context)
        {
            SeedData.Execute(context);
        }

        public void InitializeDatabase(InterceptingApplicationDbContext context)
        {
            SeedData.Execute(context);
        }
    }
}
