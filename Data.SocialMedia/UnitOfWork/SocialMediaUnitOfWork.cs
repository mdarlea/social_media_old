using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressTypeAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.EventAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.SentMessageAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamFilterAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamingEventAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Infrastructure.Data.Seedwork.UnitOfWork;

namespace Swaksoft.Infrastructure.Data.SocialMedia.UnitOfWork
{
    public abstract class SocialMediaUnitOfWork : EntityFrameworkUnitOfWork
    {
        protected SocialMediaUnitOfWork(string nameOrConnectionString) 
            : base(nameOrConnectionString)
        {
        }

        protected SocialMediaUnitOfWork() : base("SocialMediaDataSource")
        {
        }

        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; } 
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<SentMessage> SentMessages { get; set; }
        public DbSet<StreamedTweet> StreamedTweets { get; set; }
        public DbSet<StreamFilter> StreamFilters { get; set; }
        public DbSet<StreamingEvent> StreamingEvents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.AddFromAssembly(GetType().Assembly);
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
