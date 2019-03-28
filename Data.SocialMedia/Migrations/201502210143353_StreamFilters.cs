namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StreamFilters : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("UserMessageOperations", "UserId", "aspnetusers");
            //DropIndex("MessageOperations", new[] { "UserId" });
            //AddColumn("Messages", "UserId", c => c.String(maxLength: 128, storeType: "nvarchar"));
            //AddColumn("StreamFilters", "UserId", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            //CreateIndex("Messages", "UserId");
            //CreateIndex("StreamFilters", "UserId");
            //AddForeignKey("StreamFilters", "UserId", "aspnetusers", "Id", cascadeDelete: true);
            //AddForeignKey("Messages", "UserId", "aspnetusers", "Id");
            DropColumn("MessageOperations", "UserId");
            DropColumn("MessageOperations", "CustomMessage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MessageOperations", "CustomMessage", c => c.String(maxLength: 250, storeType: "nvarchar"));
            AddColumn("dbo.MessageOperations", "UserId", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            DropForeignKey("dbo.Messages", "UserId", "dbo.aspnetusers");
            DropForeignKey("dbo.StreamFilters", "UserId", "dbo.aspnetusers");
            DropIndex("dbo.StreamFilters", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropColumn("dbo.StreamFilters", "UserId");
            DropColumn("dbo.Messages", "UserId");
            CreateIndex("dbo.MessageOperations", "UserId");
            AddForeignKey("dbo.MessageOperations", "UserId", "dbo.aspnetusers", "Id", cascadeDelete: true);
        }
    }
}
