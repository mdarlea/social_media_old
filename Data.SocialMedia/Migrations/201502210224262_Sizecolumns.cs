namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sizecolumns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Messages", "UserId", c => c.String(nullable: false, maxLength: 128, storeType: "varchar"));
            AlterColumn("StreamFilters", "Query", c => c.String(nullable: false, maxLength: 150, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
        }
    }
}
