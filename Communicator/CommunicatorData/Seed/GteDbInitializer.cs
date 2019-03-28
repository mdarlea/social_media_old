using GTE.Twiml.Domain.Common.Aggregates;
using GTE.Twiml.Domain.Common.Aggregates.VoiceOperationAgg;
using GTE.Twiml.Infrastructure.Data.UnitOfWork;
using System.Collections.Generic;
using System.Data.Entity;

namespace GTE.Twiml.Infrastructure.Data.Seed
{
    public class GteDbInitializer : CreateDatabaseIfNotExists<TwilioUnitOfWork>
    {
        protected override void Seed(TwilioUnitOfWork context)
        {
            var invalidOptionMessage = new Message
            {
                Text = "You entered an invalid option"
            };
            var selectOptionMessage = new Message
            {
                Text = "Press {0} to {1}"
            };
            var messages = new HashSet<Message>
            {
                new Message {Text = "Hello, here is your one time verification code from GTE Financial {0}"}
            };
            context.Messages.AddRange(messages);

            var voiceOperation = new VoiceOperation
            {
                Description = "Hang up",
                Timeout = 3,
                NumDigits = 1,
                Action = "HangUp",
                Messages = new[] { new Message { Text = "Good Bye" } }
            };
            context.MessageOperations.Add(voiceOperation);

            var voiceOptions = new HashSet<VoiceOption>
            {
                new VoiceOption {Key = "1", Description = "repeat this message"},
                new VoiceOption {Key = "2", Description = "hang up", NextVoiceOperation = voiceOperation}
            };
            
            voiceOperation = new VoiceOperation
            {
                Description = "Validation Code Message",
                Timeout = 3,
                //FinishOnKey = "2",
                NumDigits = 1,
                MaxAttempts = 3,
                Action = "VerificationCode",
                InvalidOptionMessage = invalidOptionMessage,
                SelectOptionMessage = selectOptionMessage,
                Messages = messages,
                VoiceOptions = voiceOptions
            };
            context.MessageOperations.Add(voiceOperation);
            base.Seed(context);
        }
    }
}
