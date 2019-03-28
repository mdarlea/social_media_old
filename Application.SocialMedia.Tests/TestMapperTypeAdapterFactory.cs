using System;
using AutoMapper;
using Swaksoft.Application.Seedwork.TypeMapping;
using Swaksoft.Application.SocialMedia.TypeMapping.Profiles;
using Swaksoft.Infrastructure.AntiCorruption.Twitter.TypeMapping.Profiles;
using Swaksoft.Infrastructure.Crosscutting.TypeMapping;

namespace Application.SocialMedia.Tests
{
    public class TestMapperTypeAdapterFactory : ITypeAdapterFactory
    {
                /// <summary>
        /// Create a new Automapper type adapter factory
        /// </summary>
        public TestMapperTypeAdapterFactory()
        {
            //scan all assemblies finding Automapper Profile
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new ApplicationProfile());
                cfg.AddProfile(new TweetSharpProfile());
            });
        }

        public ITypeAdapter Create()
        {
            return new AutoMapperTypeAdapter();
        }
    }
}
