namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExternalUserIdColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "ExternalUserId", c => c.String(maxLength: 150, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "ExternalUserId");
        }
    }
}
