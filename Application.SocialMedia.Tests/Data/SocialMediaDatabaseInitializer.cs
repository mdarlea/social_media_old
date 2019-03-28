using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Application.SocialMedia.Tests.Data
{
    public partial class SocialMediaDatabaseInitializer : DropCreateDatabaseAlways<TestSocialMediaUnitOfWork>
    {
        protected override void Seed(TestSocialMediaUnitOfWork context)
        {
            SeedAddresses(context);
            SeedEvents(context);
        }

        partial void SeedAddresses(TestSocialMediaUnitOfWork context);
        partial void SeedEvents(TestSocialMediaUnitOfWork context);
    }
}
