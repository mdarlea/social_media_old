using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.MessageOperationAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamingEventAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamFilterAgg
{
    public class StreamFilter : Entity
    {
        private HashSet<StreamFilterMessageOperation> _streamFilterMessageOperations;
        public virtual ICollection<StreamFilterMessageOperation> StreamFilterMessageOperations
        {
            get
            {
                return _streamFilterMessageOperations 
                    ?? (_streamFilterMessageOperations = new HashSet<StreamFilterMessageOperation>());
            }
            set
            {
                _streamFilterMessageOperations = new HashSet<StreamFilterMessageOperation>(value);
            }
        }
        
        private HashSet<StreamingEvent> _streamingEvents;
        public virtual ICollection<StreamingEvent> StreamingEvents
        {
            get
            {
                return _streamingEvents ?? (_streamingEvents = new HashSet<StreamingEvent>());
            }
            set
            {
                _streamingEvents = new HashSet<StreamingEvent>(value);
            }
        }
        
        [Required]
        public string Query { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public bool Disabled { get; set; }

        public IEnumerable<string> GetMessages()
        {
            return from m in StreamFilterMessageOperations
                select m.Message.Text;
        } 

        public void SetUserForThisStreamFilter(User user)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (user.IsTransient())
            {
                throw new ArgumentException("Cannot associate transient or null user");
            }

            UserId = user.Id;
            User = user;
        }
    }
}
