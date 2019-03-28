namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedAddressType : DbMigration
    {
        public override void Up()
        {
            //RenameColumn(table: "dbo.Addresses", name: "AddressTypeId", newName: "AddressType_Id");
            //RenameIndex(table: "dbo.Addresses", name: "IX_AddressTypeId", newName: "IX_AddressType_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Addresses", name: "IX_AddressType_Id", newName: "IX_AddressTypeId");
            RenameColumn(table: "dbo.Addresses", name: "AddressType_Id", newName: "AddressTypeId");
        }
    }
}
