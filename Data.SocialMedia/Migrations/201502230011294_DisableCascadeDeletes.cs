namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisableCascadeDeletes : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("MessageOperations","FK_UserMessageOperations_Messages_MessageId");
            //DropForeignKey("MessageOperationsToStreamFilters", "FK_f342c11a5a0a4ddcb37a006712db8ef2");
            //DropForeignKey("MessageOperationsToStreamFilters", "StreamFilterId", "StreamFilters");
            DropForeignKey("StreamedTweetToUserProfiles", "FK_9998832e89c54c5ebb221d44c652839f");
            DropForeignKey("StreamedTweetToUserProfiles", "FK_ddcde54d1f2c4eaa8b13256864a3d972");
            DropIndex("MessageOperations", new[] { "MessageId" });
            AlterColumn("MessageOperations", "MessageId", c => c.Int(nullable: false));
            CreateIndex("MessageOperations", "MessageId");
            AddForeignKey("MessageOperations", "MessageId", "Messages", "Id", cascadeDelete: false);
            AddForeignKey("MessageOperationsToStreamFilters", "MessageOperationId", "MessageOperations", "Id");
            AddForeignKey("MessageOperationsToStreamFilters", "StreamFilterId", "StreamFilters", "Id");
            AddForeignKey("StreamedTweetToUserProfiles", "StreamedTweetId", "StreamedTweets", "Id");
            AddForeignKey("StreamedTweetToUserProfiles", "UserProfileId", "TwitterUserProfiles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StreamedTweetToUserProfiles", "UserProfileId", "dbo.TwitterUserProfiles");
            DropForeignKey("dbo.StreamedTweetToUserProfiles", "StreamedTweetId", "dbo.StreamedTweets");
            DropForeignKey("dbo.MessageOperationsToStreamFilters", "StreamFilterId", "dbo.StreamFilters");
            DropForeignKey("dbo.MessageOperationsToStreamFilters", "MessageOperationId", "dbo.MessageOperations");
            DropForeignKey("dbo.MessageOperations", "MessageId", "dbo.Messages");
            DropIndex("dbo.MessageOperations", new[] { "MessageId" });
            AlterColumn("dbo.MessageOperations", "MessageId", c => c.Int());
            CreateIndex("dbo.MessageOperations", "MessageId");
            AddForeignKey("dbo.StreamedTweetToUserProfiles", "UserProfileId", "dbo.TwitterUserProfiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StreamedTweetToUserProfiles", "StreamedTweetId", "dbo.StreamedTweets", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MessageOperationsToStreamFilters", "StreamFilterId", "dbo.StreamFilters", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MessageOperationsToStreamFilters", "MessageOperationId", "dbo.MessageOperations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MessageOperations", "MessageId", "dbo.Messages", "Id");
        }
    }
}
