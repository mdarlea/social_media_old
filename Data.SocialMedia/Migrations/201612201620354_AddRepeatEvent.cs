namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRepeatEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Repeat", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Repeat");
        }
    }
}
