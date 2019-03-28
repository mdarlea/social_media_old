using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class EventResult :ActionResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Instructor { get; set; }
        public string Description { get; set; }

        public int AddressId { get; set; }
        public string UserId { get; set; }
        public bool? Repeat { get; set; }
        public virtual DateTime StartTime { get; set; }

        public virtual DateTime EndTime { get; set; }
		
		public string RecurrencePattern { get; set; }
		public string RecurrenceException { get; set; }
	}
}
