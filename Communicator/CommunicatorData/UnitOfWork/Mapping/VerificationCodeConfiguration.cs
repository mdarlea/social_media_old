using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.VerificationCodeAgg;

namespace Swaksoft.Infrastructure.Data.Communicator.UnitOfWork.Mapping
{
    public class VerificationCodeConfiguration : EntityTypeConfiguration<VerificationCode>
    {
        public VerificationCodeConfiguration()
        {
            Property(code => code.Code).IsRequired().HasMaxLength(50);
            HasMany(code => code.VerificationCodeAlerts)
                .WithRequired(a => a.VerificationCode)
                .HasForeignKey(a => a.VerificationCodeId);

            ToTable("VerificationCodes", CommunicatorUnitOfWork.Schema);
        }
    }
}
