using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMedia.Host.Models.Account
{
    public class ExternalLoginCallbackModel
    {
        public string ClientId;
        public string Provider;
    }
}