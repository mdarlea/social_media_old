using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorPhoneNumberAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;
using Swaksoft.Infrastructure.Data.Communicator.UnitOfWork;

namespace Swaksoft.Infrastructure.Data.Communicator.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CommunicatorUnitOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CommunicatorUnitOfWork context)
        {
            var invalidOptionMessage = new Message
            {
                Text = "You entered an invalid option"
            };
            var selectOptionMessage = new Message
            {
                Text = "Press {0} to {1}"
            };
            var message = new Message {Text = "Hello, here is your code {0}"};

            context.Messages.AddOrUpdate(m => m.Text, 
                invalidOptionMessage, 
                selectOptionMessage,
                message);

            var operation = context.MessageOperations.SingleOrDefault(vo => vo.Action == "SayWord");
            if (operation != null)
            {
                context.MessageOperations.Remove(operation);    
            }

            var voiceOperation = context.MessageOperations.OfType<VoiceOperation>().FirstOrDefault(m => m.Description == "Hang up");
            if (voiceOperation == null)
            {
                voiceOperation = new VoiceOperation
                {
                    Description = "Hang up",
                    Timeout = 3,
                    NumDigits = 1,
                    Action = "HangUp",
                    Messages = new[] { new Message { Text = "Good Bye" } }
                };
                context.MessageOperations.Add(voiceOperation);    
            }
            var voiceOptions = new HashSet<VoiceOption>
            {
                new VoiceOption {Key = "1", Description = "repeat this message"},
                new VoiceOption {Key = "2", Description = "hang up", NextVoiceOperation = voiceOperation}
            };

            PhoneNumber phoneNumber;
            PhoneNumber.TryParse("321-441-3248", out phoneNumber);

            var communicatorPhoneNumber = context.CommunicatorPhoneNumbers.FirstOrDefault(p => p.PhoneNumber.Equals(phoneNumber));
            if (communicatorPhoneNumber == null)
            {
                communicatorPhoneNumber = new CommunicatorPhoneNumber
                {
                    CreateDate = DateTime.UtcNow,
                    EditDate = DateTime.UtcNow,
                    PhoneNumber = phoneNumber
                };
                context.CommunicatorPhoneNumbers.Add(communicatorPhoneNumber);
            }

            //  This method will be called after migrating to the latest version.
            context.MessageOperations.AddOrUpdate(vo => vo.Description,
                new VoiceOperation
                {
                    Action = "VerificationCode",
                    Description = "Verification Code Message",
                    Timeout = 3,
                    NumDigits = 1,
                    MaxAttempts = 3,
                    InvalidOptionMessage = invalidOptionMessage,
                    SelectOptionMessage = selectOptionMessage,
                    FromPhoneNumber = communicatorPhoneNumber,
                    Messages = new HashSet<Message> { message },
                    VoiceOptions = voiceOptions
                }, new SmsOperation
                {
                    Action = "VerificationCode",
                    Description = "Verification Code SMS",
                    Messages = new HashSet<Message>
                    {
                        new Message
                        {
                            Text = "{0} is your temporary authorization code. If you didn't request the code, please call {1}"
                        }
                    }
                });
        }
    }
}
