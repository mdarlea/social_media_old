namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    Description = c.String(unicode: false),
                    AddressId = c.Int(nullable: false),
                    StartTime = c.DateTime(nullable: false, precision: 0),
                    EndTime = c.DateTime(nullable: false, precision: 0),
                    UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: false)
                .ForeignKey("dbo.aspnetusers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.AddressId)
                .Index(t => t.UserId);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "UserId", "dbo.aspnetusers");
            DropForeignKey("dbo.Events", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Events", new[] { "UserId" });
            DropIndex("dbo.Events", new[] { "AddressId" });
            DropTable("dbo.Events");
        }
    }
}
