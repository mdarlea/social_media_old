using System.Xml.Linq;

namespace Swaksoft.Domain.Communicator.MessagingModule.Contracts
{
    public class XmlActionResult : ProviderActionResult
    {
        public XElement XmlResponse { get; set; }
    }
}
