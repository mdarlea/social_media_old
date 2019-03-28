using System;
using System.Collections.Generic;
using System.Linq;
using AntiCorruption.Twillio.Extensions;
using Swaksoft.Core;
using Swaksoft.Core.Dto;
using Swaksoft.Domain.Communicator.MessagingModule.Contracts;
using Swaksoft.Domain.Communicator.MessagingModule.Services.Providers.Twilio;
using Twilio.TwiML;

namespace AntiCorruption.Twillio
{
    public class TwimlServiceAgent : ITwimlServiceAgent
    {
        public XmlActionResult SayMessage(string url, int timeout, string finishOnKey, int numDigits, 
            string[] messages, 
            IDictionary<string, object> messagePlaceHolders)
        {
            if (timeout < 0) throw new ArgumentOutOfRangeException("timeout");
            if (numDigits < 0) throw new ArgumentOutOfRangeException("numDigits");
            if (finishOnKey != null && finishOnKey.Length > 1) throw new ArgumentOutOfRangeException("finishOnKey");

            var result = new XmlActionResult
            {
                Status = ActionResultCode.Failed
            };

            if (!messages.Any())
            {
                result.XmlResponse = (new TwilioResponse()).Element;
                return result;
            }
            
            TwilioResponse twilioResponse;
            if (string.IsNullOrEmpty(url))
            {
                twilioResponse = new TwilioResponse();
                foreach (var sayMessage in messages)
                {
                    twilioResponse.SayMessage(sayMessage, messagePlaceHolders);
                }
                //twilioResponse.Hangup();
            }
            else
            {
                twilioResponse = BeginGather(url, finishOnKey, numDigits, messages);

                foreach (var msg in messages.Select(message => message.Split(new[] { "{0}" }, StringSplitOptions.None)))
                {
                    if (msg.Length > 1)
                    {
                        foreach (var sayMessage in msg.Where(sayMessage => !string.IsNullOrWhiteSpace(sayMessage)))
                        {
                            twilioResponse.SayMessage(sayMessage);

                            //ToDo: Not Implemented Yet
                            //if (!string.IsNullOrEmpty(word))
                            //{
                            //    twilioResponse.SpellWord(word);
                            //}
                        }
                    }
                    else if (msg.Any())
                    {
                        twilioResponse.SayMessage(msg[0]);
                    }
                }

                twilioResponse.EndGather();
            }

            result.XmlResponse = twilioResponse.Element;
            result.Status = ActionResultCode.Success;

            return result;
        }

        private static TwilioResponse BeginGather(string url, string finishOnKey, int numDigits, IEnumerable<string> messages)
        {
            var twilioResponse = new TwilioResponse();

            if (!messages.Any())
            {
                return twilioResponse;
            }

            if (numDigits == 0)
            {
                if (string.IsNullOrWhiteSpace(finishOnKey))
                {
                    twilioResponse.BeginGather(new { action = url });
                }
                else
                {
                    twilioResponse.BeginGather(new { action = url, finishOnKey });
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(finishOnKey))
                {
                    twilioResponse.BeginGather(new { action = url, numDigits });
                }
                else
                {
                    twilioResponse.BeginGather(new { action = url, numDigits, finishOnKey });
                }
            }
            return twilioResponse;
        }

    }
}
