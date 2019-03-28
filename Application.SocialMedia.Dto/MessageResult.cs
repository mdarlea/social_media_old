using Swaksoft.Core;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class MessageResult : ActionResult
    {
        public int Id { get; set; }

        public string Text { get; set; }
    }
}
