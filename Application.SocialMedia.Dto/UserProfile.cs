namespace Swaksoft.Application.SocialMedia.Dto
{
    public class UserProfile
    {
        public string ProviderKey { get; set; }

        public int Id { get; set; }

        public string ExternalUserId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
