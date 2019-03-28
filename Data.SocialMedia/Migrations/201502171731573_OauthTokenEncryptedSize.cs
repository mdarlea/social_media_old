namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OauthTokenEncryptedSize : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProfiles", "AuthorizationToken_AccessToken", c => c.String(nullable: false, maxLength: 250, storeType: "nvarchar"));
            AlterColumn("dbo.UserProfiles", "AuthorizationToken_AccessTokenSecret", c => c.String(maxLength: 250, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProfiles", "AuthorizationToken_AccessTokenSecret", c => c.String(maxLength: 70, storeType: "nvarchar"));
            AlterColumn("dbo.UserProfiles", "AuthorizationToken_AccessToken", c => c.String(nullable: false, maxLength: 70, storeType: "nvarchar"));
        }
    }
}
