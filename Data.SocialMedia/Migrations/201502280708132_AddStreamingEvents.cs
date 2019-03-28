namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStreamingEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "StreamingEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "StreamingEventsToStreamFilters",
                c => new
                    {
                        StreamingEventId = c.Int(nullable: false),
                        StreamFilterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StreamingEventId, t.StreamFilterId })
                .ForeignKey("StreamingEvents", t => t.StreamingEventId)
                .ForeignKey("StreamFilters", t => t.StreamFilterId)
                .Index(t => t.StreamingEventId)
                .Index(t => t.StreamFilterId);
            
            AddColumn("StreamFilters", "Disabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("StreamingEventsToStreamFilters", "StreamFilterId", "StreamFilters");
            DropForeignKey("StreamingEventsToStreamFilters", "StreamingEventId", "StreamingEvents");
            DropIndex("StreamingEventsToStreamFilters", new[] { "StreamFilterId" });
            DropIndex("StreamingEventsToStreamFilters", new[] { "StreamingEventId" });
            DropColumn("StreamFilters", "Disabled");
            DropTable("StreamingEventsToStreamFilters");
            DropTable("StreamingEvents");
        }
    }
}
