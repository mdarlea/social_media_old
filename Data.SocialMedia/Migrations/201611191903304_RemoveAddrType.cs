namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAddrType : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Addresses", "AddressTypeId", "dbo.AddressTypes");
            //DropIndex("dbo.Addresses", new[] { "AddressTypeId" });
            //DropColumn("dbo.Addresses", "AddressTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "AddressTypeId", c => c.Int());
            CreateIndex("dbo.Addresses", "AddressTypeId");
            AddForeignKey("dbo.Addresses", "AddressTypeId", "dbo.AddressTypes", "Id");
        }
    }
}
