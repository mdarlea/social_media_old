namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlaceNameToAddress : DbMigration
    {
        public override void Up()
        {
            //DropIndex("dbo.Addresses", new[] { "AddressType_Id" });
            //DropColumn("dbo.Addresses", "AddressTypeId");
            //RenameColumn(table: "dbo.Addresses", name: "AddressType_Id", newName: "AddressTypeId");
            AddColumn("dbo.Addresses", "PlaceName", c => c.String(maxLength: 100, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "PlaceName");
            //RenameColumn(table: "dbo.Addresses", name: "AddressTypeId", newName: "AddressType_Id");
            //AddColumn("dbo.Addresses", "AddressTypeId", c => c.Int());
            //CreateIndex("dbo.Addresses", "AddressType_Id");
        }
    }
}
