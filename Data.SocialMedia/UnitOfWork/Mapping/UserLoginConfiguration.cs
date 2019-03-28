using System;
using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;

namespace Swaksoft.Infrastructure.Data.SocialMedia.UnitOfWork.Mapping
{
    public class UserLoginConfiguration : EntityTypeConfiguration<UserLogin>
    {
        public UserLoginConfiguration()
        {
            HasRequired(e => e.User)
                .WithMany(u => u.UserLogins)
                .HasForeignKey(e => e.UserId);

            Map<TwitterUserLogin>(m => m.Requires("LoginProvider").HasValue("Twitter"));
            Map<FacebookUserLogin>(m => m.Requires("LoginProvider").HasValue("Facebook"));

            ToTable("aspnetuserlogins");
        }
    }
}
