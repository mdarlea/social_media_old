namespace Swaksoft.Application.SocialMedia.Dto
{
    public class MessageOperationRequest
    {
        public string UserId { get; set; }

        public int? MessageOperationId { get; set; }
        public int? MessageId { get; set; }

        public string Message { get; set; }

        public int? StreamFilterId { get; set; }
        public string Query { get; set; }
        
        public MessageOperationType Type { get; set; }
    }
}
