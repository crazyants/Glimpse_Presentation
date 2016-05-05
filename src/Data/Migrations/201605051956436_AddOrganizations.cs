namespace Kiehl.App.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddOrganizations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        FeatureId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.FeatureId)
                .Index(t => t.Name, unique: true);

            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        OrganizationId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        GroupName = c.String(nullable: false, maxLength: 255),
                        Abbreviation = c.String(nullable: false, maxLength: 255),
                        Phone = c.String(maxLength: 128),
                        AddressLine1 = c.String(nullable: false, maxLength: 255),
                        AddressLine2 = c.String(maxLength: 255),
                        City = c.String(nullable: false, maxLength: 255),
                        State = c.String(nullable: false, maxLength: 255),
                        PostalCode = c.String(nullable: false, maxLength: 255),
                        FiscalContactSamAccountName = c.String(nullable: false, maxLength: 255),
                        ITConactSamAccountName = c.String(nullable: false, maxLength: 255),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.OrganizationId)
                .ForeignKey("dbo.Organizations", t => t.Parent_Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.GroupName, unique: true)
                .Index(t => t.Abbreviation, unique: true)
                .Index(t => t.Parent_Id);

            CreateTable(
                "dbo.OrganizationFeatures",
                c => new
                    {
                        OrganizationId = c.Int(nullable: false),
                        FeatureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrganizationId, t.FeatureId })
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .ForeignKey("dbo.Features", t => t.FeatureId, cascadeDelete: true)
                .Index(t => t.OrganizationId)
                .Index(t => t.FeatureId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.OrganizationFeatures", "FeatureId", "dbo.Features");
            DropForeignKey("dbo.OrganizationFeatures", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Organizations", "Parent_Id", "dbo.Organizations");
            DropIndex("dbo.OrganizationFeatures", new[] { "FeatureId" });
            DropIndex("dbo.OrganizationFeatures", new[] { "OrganizationId" });
            DropIndex("dbo.Organizations", new[] { "Parent_Id" });
            DropIndex("dbo.Organizations", new[] { "Abbreviation" });
            DropIndex("dbo.Organizations", new[] { "GroupName" });
            DropIndex("dbo.Organizations", new[] { "Name" });
            DropIndex("dbo.Features", new[] { "Name" });
            DropTable("dbo.OrganizationFeatures");
            DropTable("dbo.Organizations");
            DropTable("dbo.Features");
        }
    }
}
