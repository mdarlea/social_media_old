using System;

/*! \brief The [Messaging Application Services] (\ref Swaksoft.Application.Communicator.MessagingModule.Services) aggregate
 * Defines the application services for sending the following message types:
 * 
 */ 
namespace Swaksoft.Application.Communicator.MessagingModule.Services
{
    public interface ICommunicatorAppService : IDisposable
    {
        Dto.MessageOperationResult VerificationCode(Dto.VerificationCode message);
    }
}