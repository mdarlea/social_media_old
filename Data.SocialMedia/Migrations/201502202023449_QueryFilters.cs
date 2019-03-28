namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QueryFilters : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("QueryFilters", "MessageOperationId", "MessageOperations");
            DropIndex("QueryFilters", new[] { "MessageOperationId" });
            CreateTable(
                "FilterUserMessageOperationsToQueryFilters",
                c => new
                    {
                        FilterUserMessageOperationId = c.Int(nullable: false),
                        QueryFilterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilterUserMessageOperationId, t.QueryFilterId })
                .ForeignKey("UserMessageOperations", t => t.FilterUserMessageOperationId, cascadeDelete: true)
                .ForeignKey("QueryFilters", t => t.QueryFilterId, cascadeDelete: true)
                .Index(t => t.FilterUserMessageOperationId)
                .Index(t => t.QueryFilterId);
            
            DropColumn("QueryFilters", "MessageOperationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QueryFilters", "MessageOperationId", c => c.Int(nullable: false));
            DropForeignKey("dbo.FilterUserMessageOperationsToQueryFilters", "QueryFilterId", "dbo.QueryFilters");
            DropForeignKey("dbo.FilterUserMessageOperationsToQueryFilters", "FilterUserMessageOperationId", "dbo.UserMessageOperations");
            DropIndex("dbo.FilterUserMessageOperationsToQueryFilters", new[] { "QueryFilterId" });
            DropIndex("dbo.FilterUserMessageOperationsToQueryFilters", new[] { "FilterUserMessageOperationId" });
            DropTable("dbo.FilterUserMessageOperationsToQueryFilters");
            CreateIndex("dbo.QueryFilters", "MessageOperationId");
            AddForeignKey("dbo.QueryFilters", "MessageOperationId", "dbo.UserMessageOperations", "Id", cascadeDelete: true);
        }
    }
}
