using System.Collections.Generic;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class StreamFilter
    {
        public StreamFilter()
        {
            MessageOperations = new List<MessageOperation>();
        }

        public int Id { get; set; }
        public string Query { get; set; }
        public List<MessageOperation> MessageOperations { get; set; }
    }
}
