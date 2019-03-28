namespace Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StreetAddress = c.String(maxLength: 100, storeType: "nvarchar"),
                        SuiteNumber = c.String(maxLength: 10, storeType: "nvarchar"),
                        City = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        State = c.String(maxLength: 2, storeType: "nvarchar"),
                        Zip = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        CountryIsoCode = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        GeolocationStreetNumber = c.Int(nullable: false),
                        GeolocationStreet = c.String(maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Secret = c.String(nullable: false, unicode: false),
                        Name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        ApplicationType = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        RefreshTokenLifeTime = c.Int(nullable: false),
                        AllowedOrigin = c.String(maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RefreshTokens",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Subject = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        ClientId = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        IssuedUtc = c.DateTime(nullable: false, precision: 0),
                        ExpiresUtc = c.DateTime(nullable: false, precision: 0),
                        ProtectedTicket = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ApplicationUser_Id = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.aspnetusers", t => t.ApplicationUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.aspnetusers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name_FirstName = c.String(nullable: false, maxLength: 150, storeType: "nvarchar"),
                        Name_LastName = c.String(nullable: false, maxLength: 150, storeType: "nvarchar"),
                        Name_MiddleName = c.String(maxLength: 150, storeType: "nvarchar"),
                        Hometown = c.String(unicode: false),
                        FacebookId = c.String(maxLength: 100, storeType: "nvarchar"),
                        SkypeId = c.String(maxLength: 100, storeType: "nvarchar"),
                        Email = c.String(unicode: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(unicode: false),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                        ApplicationUser_Id = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.aspnetusers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ApplicationUser_Id = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.aspnetusers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AddressUsers",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Address_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Address_Id })
                .ForeignKey("dbo.aspnetusers", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Addresses", t => t.Address_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Address_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "ApplicationUser_Id", "dbo.aspnetusers");
            DropForeignKey("dbo.AspNetUserLogins", "ApplicationUser_Id", "dbo.aspnetusers");
            DropForeignKey("dbo.AspNetUserClaims", "ApplicationUser_Id", "dbo.aspnetusers");
            DropForeignKey("dbo.AddressUsers", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.AddressUsers", "User_Id", "dbo.aspnetusers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AddressUsers", new[] { "Address_Id" });
            DropIndex("dbo.AddressUsers", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AddressUsers");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.aspnetusers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RefreshTokens");
            DropTable("dbo.Clients");
            DropTable("dbo.Addresses");
        }
    }
}
