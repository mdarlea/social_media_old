using System;
using System.ComponentModel.DataAnnotations;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg
{
    public class TwilioAuthToken : ValueObject<TwilioAuthToken>
    {
        public TwilioAuthToken()
        {
        }

        public TwilioAuthToken(string accountSid, string authToken)
        {
            if (string.IsNullOrEmpty(accountSid)) throw new ArgumentNullException("accountSid");
            if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");

            AccountSid = accountSid;
            AuthToken = authToken;
        }

        [Required]
        public string AccountSid { get; private set; }

        [Required]
        public string AuthToken { get; private set; }

        #region equality
        public override bool Equals(TwilioAuthToken other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && string.Equals(AccountSid, other.AccountSid) && string.Equals(AuthToken, other.AuthToken);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode*397) ^ (AccountSid != null ? AccountSid.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (AuthToken != null ? AuthToken.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion equality
    }
}
