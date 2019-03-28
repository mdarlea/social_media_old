using Swaksoft.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Domain.Communicator.MessagingModule.Aggregates.VerificationCodeAgg
{
    /// <summary>
    /// Aggregate Root
    /// </summary>
    public class VerificationCode : Entity
    {
        [Required]
        public virtual string Code { get; set; }
        public virtual ICollection<VerificationCodeSent> VerificationCodeAlerts { get; set; }

        public bool IsValid(int length)
        {
            if(length < 1) throw new ArgumentOutOfRangeException("length");

            //validates the code
            return (Code != null) && Code.Trim().Length == length;
        }

        public void GenerateNewCode(int length)
        {
            if(length < 1) throw new ArgumentOutOfRangeException("length");

            if (!IsTransient()) return;

            const string chars = "0123456789";
            var random = new Random();
            Code =  new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
