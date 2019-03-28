using System;
using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;

namespace Swaksoft.Infrastructure.Data.SocialMedia.UnitOfWork.Mapping
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(e => e.Email).HasMaxLength(255);
            Property(e => e.UserName).HasMaxLength(255);

            HasMany(u => u.Addresses)
                .WithMany()
                    .Map(map =>
                    {
                        map.MapLeftKey("User_Id");
                        map.MapRightKey("Address_Id");
                        map.ToTable("AddressUsers");
                    });

            ToTable("aspnetusers");
        }
    }
}
