namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddressTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddressTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Addresses", "IsMainAddress", c => c.Boolean(nullable: false));
            AddColumn("dbo.Addresses", "AddressTypeId", c => c.Int(nullable: true));
            CreateIndex("dbo.Addresses", "AddressTypeId");
            AddForeignKey("dbo.Addresses", "AddressTypeId", "dbo.AddressTypes", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "AddressTypeId", "dbo.AddressTypes");
            DropIndex("dbo.Addresses", new[] { "AddressTypeId" });
            DropColumn("dbo.Addresses", "AddressTypeId");
            DropColumn("dbo.Addresses", "IsMainAddress");
            DropTable("dbo.AddressTypes");
        }
    }
}
