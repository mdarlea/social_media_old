using System;
using Swaksoft.Core;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class MessageOperationResult : ActionResult
    {
        public int MessageOperationId { get; set; }
        public int MessageId { get; set; }
        public string Type { get; set; }
    }
}
