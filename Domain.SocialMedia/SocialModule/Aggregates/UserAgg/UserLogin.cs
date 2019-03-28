using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg
{
    public abstract class UserLogin
    {
        [Key, Column(Order = 0)]
        public string ProviderKey { get; set; }

        [Key, Column(Order = 1)]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
