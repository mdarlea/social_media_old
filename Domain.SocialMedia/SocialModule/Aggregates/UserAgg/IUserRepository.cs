using System;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserWithUserProfiles(string userId);

        User GetUserWithMessageOperations(string userId);

        User GetUserWithMessages(string userId);
        User GetUserWithAddresses(string userId);
        User Get(string id);
    }
}
