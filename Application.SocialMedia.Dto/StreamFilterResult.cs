using System;
using Swaksoft.Core;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class StreamFilterResult : ActionResult
    {
        public int Id { get; set; }
        public string Query { get; set; }
    }
}
