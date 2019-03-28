namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddrType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "AddressTypeId", c => c.Int());
            CreateIndex("dbo.Addresses", "AddressTypeId");
            AddForeignKey("dbo.Addresses", "AddressTypeId", "dbo.AddressTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "AddressTypeId", "dbo.AddressTypes");
            DropIndex("dbo.Addresses", new[] { "AddressTypeId" });
            DropColumn("dbo.Addresses", "AddressTypeId");
        }
    }
}
