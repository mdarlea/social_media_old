using System;
using System.Linq;
using Twilio.TwiML;

namespace AntiCorruption.Twillio.Extensions
{
    public static class TwilioResponseExtensions
    {
        public static bool SayMessage(this TwilioResponse twilioResponse, string message, params object[] messageArgs)
        {
            if (twilioResponse == null) throw new ArgumentNullException("twilioResponse");
            if (String.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

            if (messageArgs == null || !messageArgs.Any())
            {
                twilioResponse.Say(message, new { voice = "woman" });
            }
            else
            {
                twilioResponse.Say(string.Format(message, messageArgs), new { voice = "woman" });
            }
            return true;
        }

        public static bool SpellWord(this TwilioResponse twilioResponse, string message, params object[] messageArgs)
        {
            if (twilioResponse == null) throw new ArgumentNullException("twilioResponse");
            if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

            foreach (var character in message)
            {
                twilioResponse.Say(character.ToString(), new { voice = "woman" });
                twilioResponse.Pause();
            }

            return true;
        }
    }
}
