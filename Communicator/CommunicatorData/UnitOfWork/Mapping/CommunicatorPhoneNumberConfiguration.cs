using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorPhoneNumberAgg;

namespace Swaksoft.Infrastructure.Data.Communicator.UnitOfWork.Mapping
{
    public class CommunicatorPhoneNumberConfiguration : EntityTypeConfiguration<CommunicatorPhoneNumber>
    {
        public CommunicatorPhoneNumberConfiguration()
        {
            Property(v => v.CreateDate).IsRequired();
            Property(v => v.EditDate).IsRequired();

            //HasRequired(v => v.PhoneNumber);

            ToTable("CommunicatorPhoneNumbers", CommunicatorUnitOfWork.Schema);
        }
    }
}
