namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserMessageOperation : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "MessageOperations", newName: "UserMessageOperations");
            DropForeignKey("UserToMessageOperations", "MessageId", "Messages");
            DropForeignKey("UserToMessageOperations", "MessageOperationId", "MessageOperations");
            DropForeignKey("UserToMessageOperations", "UserProfileId", "UserProfiles");
            DropIndex("UserToMessageOperations", new[] { "UserId" });
            DropIndex("UserToMessageOperations", new[] { "MessageOperationId" });
            DropIndex("UserToMessageOperations", new[] { "MessageId" });
            AddColumn("UserMessageOperations", "UserId", c => c.String(nullable: false, maxLength: 128, storeType: "nvarchar"));
            AddColumn("UserMessageOperations", "MessageId", c => c.Int());
            AddColumn("UserMessageOperations", "CustomMessage", c => c.String(maxLength: 250, storeType: "nvarchar"));
            AlterColumn("UserProfiles", "Provider", c => c.String(maxLength: 128, storeType: "nvarchar"));
            CreateIndex("UserMessageOperations", "UserId");
            CreateIndex("UserMessageOperations", "MessageId");
            AddForeignKey("UserMessageOperations", "MessageId", "Messages", "Id");
            AlterColumn("UserMessageOperations", "UserId", c => c.String(maxLength: 125, storeType: "varchar", nullable:false));
            AddForeignKey("UserMessageOperations", "UserId", "dbo.aspnetusers", "Id", cascadeDelete: true);
            DropColumn("UserMessageOperations", "Description");
            DropTable("UserToMessageOperations");
        }
        
        public override void Down()
        {
        }
    }
}
