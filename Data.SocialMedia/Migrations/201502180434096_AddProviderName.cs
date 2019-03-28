namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProviderName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "Provider", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "Provider");
        }
    }
}
