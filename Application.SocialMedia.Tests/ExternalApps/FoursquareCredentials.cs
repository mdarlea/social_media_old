using Swaksoft.Core;
using Swaksoft.Core.External;

namespace Application.SocialMedia.Tests.ExternalApps
{
    public class FoursquareCredentials : ExternalProviderCredentials
    {
        public FoursquareCredentials()
            : base(ExternalProvider.Foursquare, 
                   consumerKey: "YGEUJPQ5N5BRIFPWOG3DKSZHTAIYKTR3OSLU4VVNDJXOTDT4", 
                   consumerSecret: "0UESNGLDS2RHHJNDKGY5CJKBLWJ0ANPEQGURKDIVNSRPMDSK")
        {
        }
    }
}
