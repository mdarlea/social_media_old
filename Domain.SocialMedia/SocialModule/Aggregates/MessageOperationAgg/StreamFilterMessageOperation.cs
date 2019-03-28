using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Swaksoft.Domain.Seedwork.Extensions;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamFilterAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.MessageOperationAgg
{
    public class StreamFilterMessageOperation : MessageOperation, IValidatableObject
    {
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return this.ValidationResults().Execute();
        }
    }
}
