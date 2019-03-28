using System;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.VerificationCodeAgg;

namespace Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg
{
    public class VerificationCodeSent : AlertSentEvent
    {
        public virtual int VerificationCodeId { get; set; }
        public virtual VerificationCode VerificationCode { get; set; }
        public virtual long MemberNbr { get; set; }

        public void SetVerificationCode(VerificationCode verificationCode)
        {
            if (verificationCode == null) throw new ArgumentNullException("verificationCode");

            VerificationCode = verificationCode;
            VerificationCodeId = verificationCode.Id;
        }

        public VerificationCode SetNewVerificationCode(int verificationCodeLength)
        {
            var code = new VerificationCode();
            code.GenerateNewCode(verificationCodeLength);
            SetVerificationCode(code);
            return code;
        }
    }
}
