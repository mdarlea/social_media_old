using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.Seedwork.Aggregates.ProfileAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg;

namespace Swaksoft.Infrastructure.Data.Communicator.UnitOfWork.Mapping
{
    public partial class ProfileConfiguration : EntityTypeConfiguration<Profile>
    {
        public ProfileConfiguration()
        {
            Property(p => p.Name).HasMaxLength(50);
            MapProviderProfiles();
            ToTable("CommunicatorProfiles", CommunicatorUnitOfWork.Schema);
        }
        partial void MapProviderProfiles();
    }

    public class CommunicatorProfileConfiguration : EntityTypeConfiguration<CommunicatorProfile>
    {
        public CommunicatorProfileConfiguration()
        {
            HasRequired(p => p.DefaultPhoneNumber).WithMany().HasForeignKey(p => p.DefaultPhoneNumberId);
            HasMany(p => p.AllowNumbers).WithRequired().HasForeignKey(p => p.ProfileId);
        }
    }
}
