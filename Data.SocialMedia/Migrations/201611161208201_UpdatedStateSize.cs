namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedStateSize : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "State", c => c.String(maxLength: 30, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Addresses", "State", c => c.String(maxLength: 2, storeType: "nvarchar"));
        }
    }
}
