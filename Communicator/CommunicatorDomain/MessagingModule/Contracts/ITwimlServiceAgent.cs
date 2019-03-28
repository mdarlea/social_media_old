using System.Collections.Generic;

namespace Swaksoft.Domain.Communicator.MessagingModule.Contracts
{
    public interface ITwimlServiceAgent
    {
        XmlActionResult SayMessage(string url, int timeout, string finishOnKey, int numDigits, string[] messages, IDictionary<string, object> messagePlaceHolders);
    }

}
