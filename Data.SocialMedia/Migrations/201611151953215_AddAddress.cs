namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StreetAddress = c.String(maxLength: 50, storeType: "nvarchar"),
                        SuiteNumber = c.String(maxLength: 10, storeType: "nvarchar"),
                        GeolocationStreetNumber = c.Int(nullable: false),
                        GeolocationStreet = c.String(maxLength: 50, storeType: "nvarchar"),
                        City = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        State = c.String(maxLength: 2, storeType: "nvarchar"),
                        Zip = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        CountryIsoCode = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AddressUsers",
                c => new
                    {
                        Address_Id = c.Int(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.Address_Id, t.User_Id })
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .ForeignKey("dbo.aspnetusers", t => t.User_Id)
                .Index(t => t.Address_Id)
                .Index(t => t.User_Id);
            }
        
        public override void Down()
        {
            DropForeignKey("dbo.AddressUsers", "User_Id", "dbo.aspnetusers");
            DropForeignKey("dbo.AddressUsers", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.AddressUsers", new[] { "User_Id" });
            DropIndex("dbo.AddressUsers", new[] { "Address_Id" });
            DropTable("dbo.AddressUsers");
            DropTable("dbo.Addresses");
        }
    }
}
