using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamFilterAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamingEventAgg
{
    public enum StreamingEventType
    {
        DisplayActivityOnMap
    }

    public class StreamingEvent : Entity
    {
        [Required]
        public StreamingEventType Code { get; set; }
        public string Description { get; set; }

        private HashSet<StreamFilter> _streamFilters;
        public virtual ICollection<StreamFilter> StreamFilters
        {
            get
            {
                return _streamFilters ?? (_streamFilters = new HashSet<StreamFilter>());
            }
            set
            {
                _streamFilters = new HashSet<StreamFilter>(value);
            }
        }
    }
}
