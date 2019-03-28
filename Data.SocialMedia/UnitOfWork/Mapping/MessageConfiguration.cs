using System;
using System.Data.Entity.ModelConfiguration;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.MessageAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;

namespace Swaksoft.Infrastructure.Data.SocialMedia.UnitOfWork.Mapping
{
    public class MessageConfiguration : EntityTypeConfiguration<Message>
    {
        public MessageConfiguration()
        {
            HasRequired(e => e.User)
             .WithMany(e => e.Messages)
             .HasForeignKey(e => e.UserId);

            Property(e => e.Text).HasMaxLength(250).IsRequired();
        }
    }
}
