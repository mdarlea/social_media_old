using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace SocialMedia.Host.Models
{
    [Serializable]
    public class UserModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public IList<string> Roles { get; set; }

        [ScriptIgnore]
        public IList<System.Security.Claims.Claim> Claims { get; set; }
    }

    [Serializable]
    public class ApplicationUserModel : UserModel
    {
        public string FullName { get; set; }
    }
}