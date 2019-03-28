namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddressTypes1 : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Addresses", "AddressTypeId", "dbo.AddressTypes");
            //DropIndex("dbo.Addresses", new[] { "AddressTypeId" });
            //AlterColumn("dbo.Addresses", "AddressTypeId", c => c.Int());
            //CreateIndex("dbo.Addresses", "AddressTypeId");
            //AddForeignKey("dbo.Addresses", "AddressTypeId", "dbo.AddressTypes", "Id");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "AddressType_Id", "dbo.AddressTypes");
            DropForeignKey("dbo.Addresses", "AddressTypeId", "dbo.AddressTypes");
            DropIndex("dbo.Addresses", new[] { "AddressType_Id" });
            DropIndex("dbo.Addresses", new[] { "AddressTypeId" });
            AlterColumn("dbo.Addresses", "AddressTypeId", c => c.Int(nullable: false));
            DropColumn("dbo.Addresses", "AddressType_Id");
            CreateIndex("dbo.Addresses", "AddressTypeId");
            AddForeignKey("dbo.Addresses", "AddressTypeId", "dbo.AddressTypes", "Id", cascadeDelete: true);
        }
    }
}
