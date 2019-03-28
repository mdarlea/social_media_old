using System.Data.Entity.Migrations;

namespace GTE.Communicator.Infrastructure.Data.Migrations
{
    public class CreateVerificationCodeVoiceCall : DbMigration
    {
        public override void Up()
        {
             CreateTable(
                "dbo.VerificationCodeVoiceCalls",
                 c => new
                 {
                      Id = c.Int(nullable: false, identity: true),
                      VerificationCodeId = c.Int(nullable: false)
                  })
                  .PrimaryKey(t => t.Id)
                  .ForeignKey("dbo.VoiceCalls", arg => arg.Id)
                  .ForeignKey("dbo.VerificationCodes", arg => arg.VerificationCodeId);
        }

        public override void Down()
        {
            DropTable("dbo.VerificationCodeVoiceCalls");
        }
    }
}
