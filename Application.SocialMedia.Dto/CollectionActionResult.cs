using System;
using System.Collections.Generic;
using Swaksoft.Core;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class CollectionActionResult<T> : ActionResult
    {
        public CollectionActionResult()
        {
            Items = new List<T>();
        }

        public List<T> Items { get; set; } 
    }
}
