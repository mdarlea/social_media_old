using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class EventForUserRequest
    {
        public string UserId { get; set; }
        public TimeRange TimeRange { get; set; }
    }
}
