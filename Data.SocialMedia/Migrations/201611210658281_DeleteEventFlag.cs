namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteEventFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "IsDeleted", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "IsDeleted");
        }
    }
}
