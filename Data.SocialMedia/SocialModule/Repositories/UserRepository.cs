using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Specification;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;
using Swaksoft.Infrastructure.Data.Seedwork.Repositories;

namespace Swaksoft.Infrastructure.Data.SocialMedia.SocialModule.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ITransactionUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
        public virtual User Get(string id)
        {
            return !string.IsNullOrWhiteSpace(id) ? GetSet().Find(id) : null;
        }
        public override IQueryable<User> AllMatching(ISpecification<User> specification)
        {
            return base.AllMatching(specification).AsNoTracking();
        }

        public User GetUserWithUserProfiles(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException("userId");

            return Context.Set<User>()
                      .Include(u => u.UserProfiles)
                      .AsNoTracking()
                      .SingleOrDefault(u => u.Id == userId);
        }

        public User GetUserWithAddresses(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));

            return Context.Set<User>()
                      .Include(u => u.Addresses)
                      .SingleOrDefault(u=>u.Id == userId);
        }

        public User GetUserWithMessageOperations(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException("userId");

            return Context.Set<User>()
                       .Include(u => u.Messages.Select(m=>m.MessageOperations))
                       .SingleOrDefault(u => u.Id == userId);
        }

        public User GetUserWithMessages(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException("userId");

            return Context.Set<User>()
                          .Include(u => u.Messages)
                          .SingleOrDefault(u => u.Id == userId);
        }
    }
}
