namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRecurrence : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "RecurrencePattern", c => c.String(unicode: false));
            AddColumn("dbo.Events", "RecurrenceException", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "RecurrenceException");
            DropColumn("dbo.Events", "RecurrencePattern");
        }
    }
}
