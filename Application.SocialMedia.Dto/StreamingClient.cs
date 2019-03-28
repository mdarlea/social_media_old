using System;
using System.Collections.Generic;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class StreamingClient
    {
        public int UserProfileId { set; get; }
        
        public string UserName { get; set; }

        public string ClientName { get; set; }

        public string ClientState { get; set; }

        public IEnumerable<string> Tracks { get; set; }
    }
}
