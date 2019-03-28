namespace Swaksoft.Application.SocialMedia.Dto
{
    public class UserProfileResult : AccessTokenResult
    {
        public int Id { get; set; }
      
        public string Name { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
