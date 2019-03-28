using System;
using Swaksoft.Application.Communicator.Dto;

namespace Swaksoft.Application.Communicator.MessagingModule.Services
{
    public interface IXmlVoiceMessagingAppService : IDisposable
    {
        XmlActionResult VerificationCode(VerificationCodeRequest request);
    }
}