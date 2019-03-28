namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStreamingEvents : DbMigration
    {
        public override void Up()
        {
            AlterColumn("StreamingEvents", "Code", c => c.Int(nullable: false));
            AlterColumn("StreamingEvents", "Description", c => c.String(maxLength: 250, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("StreamingEvents", "Description", c => c.String(unicode: false));
            AlterColumn("StreamingEvents", "Code", c => c.String(unicode: false));
        }
    }
}
