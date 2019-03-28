namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddUserId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("UserProfiles", "UserId", "Users");
            DropIndex("UserProfiles", new[] { "UserId" });
            DropTable("Users");

            AlterColumn("UserProfiles", "UserId", c => c.String(maxLength: 125, storeType: "varchar"));
            CreateIndex("UserProfiles", "UserId");
            AddForeignKey("UserProfiles", "UserId", "dbo.aspnetusers", "Id");
        }
        
        public override void Down()
        {
        }
    }
}
