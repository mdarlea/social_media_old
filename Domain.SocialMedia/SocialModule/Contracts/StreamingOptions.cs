using System.Collections.Generic;
using Swaksoft.Core.External;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Contracts
{
    public class StreamingOptions
    {
        public StreamingOptions()
        {
            ClientSettings = new ClientSettings();
            Queries = new List<string>();
        }

        public ExternalProviderCredentials ClientCredentials { get; set; }

        public ClientSettings ClientSettings { get; set; }

        public ExternalUserOptions ExternalUserOptions { get; set; }

        public IEnumerable<string> Queries { get; set;}

    }
}
