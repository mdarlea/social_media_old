using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMedia.Host.Models.Account
{
  public class ExternalLoginRequestModel
  {
    public string Provider { get; set; }
    public string CallbackUrl { get; set; }
  }
}