using System;
using Swaksoft.Core;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class PagedListActionResult : ActionResult
    {
        public PagedList Items { get; set; } 
    }
}
