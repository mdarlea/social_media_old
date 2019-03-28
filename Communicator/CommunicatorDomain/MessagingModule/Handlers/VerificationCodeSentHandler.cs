using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Communicator.MessagingModule.Events;
using Swaksoft.Domain.Seedwork.Events;

namespace Swaksoft.Domain.Communicator.MessagingModule.Handlers
{
    public class VerificationCodeSentHandler : HandlerBase<VerificationCodeSent> 
    {
        public override void Handle(VerificationCodeSent args)
        {
            var @event = args.CommunicationLog.ChangeAlertSentEvent<Aggregates.CommunicationLogAgg.VerificationCodeSent>();
            @event.SetVerificationCode(args.VerificationCode);
            @event.MemberNbr = args.MemberNbr;    
        }
    }
}
