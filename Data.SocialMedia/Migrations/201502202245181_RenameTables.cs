namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameTables : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "UserMessageOperations", newName: "MessageOperations");
            //DropForeignKey("FilterUserMessageOperationsToQueryFilters", "FilterUserMessageOperationId", "UserMessageOperations");
            //DropForeignKey("FilterUserMessageOperationsToQueryFilters", "QueryFilterId", "QueryFilters");
            //DropIndex("FilterUserMessageOperationsToQueryFilters", new[] { "FilterUserMessageOperationId" });
            //DropIndex("FilterUserMessageOperationsToQueryFilters", new[] { "QueryFilterId" });
            CreateTable(
                "StreamFilters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Query = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "MessageOperationsToStreamFilters",
                c => new
                    {
                        MessageOperationId = c.Int(nullable: false),
                        StreamFilterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MessageOperationId, t.StreamFilterId })
                .ForeignKey("MessageOperations", t => t.MessageOperationId, cascadeDelete: true)
                .ForeignKey("StreamFilters", t => t.StreamFilterId, cascadeDelete: true)
                .Index(t => t.MessageOperationId)
                .Index(t => t.StreamFilterId);
            
            //DropTable("QueryFilters");
            //DropTable("FilterUserMessageOperationsToQueryFilters");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FilterUserMessageOperationsToQueryFilters",
                c => new
                    {
                        FilterUserMessageOperationId = c.Int(nullable: false),
                        QueryFilterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilterUserMessageOperationId, t.QueryFilterId });
            
            CreateTable(
                "dbo.QueryFilters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Query = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.MessageOperationsToStreamFilters", "StreamFilterId", "dbo.StreamFilters");
            DropForeignKey("dbo.MessageOperationsToStreamFilters", "MessageOperationId", "dbo.MessageOperations");
            DropIndex("dbo.MessageOperationsToStreamFilters", new[] { "StreamFilterId" });
            DropIndex("dbo.MessageOperationsToStreamFilters", new[] { "MessageOperationId" });
            DropTable("dbo.MessageOperationsToStreamFilters");
            DropTable("dbo.StreamFilters");
            CreateIndex("dbo.FilterUserMessageOperationsToQueryFilters", "QueryFilterId");
            CreateIndex("dbo.FilterUserMessageOperationsToQueryFilters", "FilterUserMessageOperationId");
            AddForeignKey("dbo.FilterUserMessageOperationsToQueryFilters", "QueryFilterId", "dbo.QueryFilters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FilterUserMessageOperationsToQueryFilters", "FilterUserMessageOperationId", "dbo.UserMessageOperations", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.MessageOperations", newName: "UserMessageOperations");
        }
    }
}
