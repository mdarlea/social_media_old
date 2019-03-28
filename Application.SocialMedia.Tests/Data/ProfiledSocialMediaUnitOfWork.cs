using System;
using System.Data.Entity;
using Swaksoft.Infrastructure.Data.SocialMedia.UnitOfWork;

namespace Application.SocialMedia.Tests.Data
{
    public class ProfiledSocialMediaUnitOfWork : SocialMediaUnitOfWork
    {
        public ProfiledSocialMediaUnitOfWork()
        {
            Database.SetInitializer<ProfiledSocialMediaUnitOfWork>(null);
            Database.Log = Console.WriteLine;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var type = typeof(SocialMediaUnitOfWork);
            modelBuilder.Configurations.AddFromAssembly(type.Assembly);
        }
    }

    public class TestSocialMediaUnitOfWork : SocialMediaUnitOfWork
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var type = typeof(SocialMediaUnitOfWork);
            modelBuilder.Configurations.AddFromAssembly(type.Assembly);
        }
    }
}
