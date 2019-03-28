using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class EventWithAddress : Event
    {
        public Dto.Address Address { get; set; }
    }
}
