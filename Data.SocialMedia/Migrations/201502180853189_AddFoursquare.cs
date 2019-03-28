namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFoursquare : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProfiles", "Provider", c => c.String(maxLength: 50, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProfiles", "Provider", c => c.String(unicode: false));
        }
    }
}
