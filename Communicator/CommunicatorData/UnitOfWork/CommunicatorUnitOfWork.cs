using Swaksoft.Infrastructure.Data.Seedwork.UnitOfWork;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorPhoneNumberAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.VerificationCodeAgg;
using System.Data.Entity;

namespace Swaksoft.Infrastructure.Data.Communicator.UnitOfWork
{
    public partial class CommunicatorUnitOfWork : EntityFrameworkUnitOfWork
    {
        public const string Schema = "Communicator";

        public CommunicatorUnitOfWork() : base("TwilerDataSource")
        {
        }

        public DbSet<CommunicatorProfile> CommuniacatorProfiles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<VoiceOption> VoiceOptions { get; set; }
        public DbSet<MessageOperation> MessageOperations { get; set; }
        public DbSet<CommunicationLog> CommunicationLogs { get; set; }
        public DbSet<AlertSentEvent> AlertSentEvents { get; set; }
        public DbSet<CommunicatorPhoneNumber> CommunicatorPhoneNumbers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
