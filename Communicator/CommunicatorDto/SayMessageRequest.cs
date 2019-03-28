
namespace Swaksoft.Application.Communicator.Dto
{
    public class SayMessageRequest : TwimlRequest
    {
        public string Action { get; set; }
        public int? Option { get; set; }
    }
}
