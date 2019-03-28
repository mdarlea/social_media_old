using System;

namespace SocialMedia.Host.Models
{
    public class AuthUserModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public DateTime Expires { get; set; }
    }
}