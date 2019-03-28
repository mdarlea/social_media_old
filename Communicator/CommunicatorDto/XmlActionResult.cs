using System.Xml.Linq;
using Swaksoft.Core;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.Communicator.Dto
{
    public class XmlActionResult : ActionResult
    {
        public XElement XmlResponse { get; set; }
    }
}
