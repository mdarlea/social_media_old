using System;
using System.Data.Entity;
using System.Linq;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Specification;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Infrastructure.Data.Seedwork.Repositories;


namespace Swaksoft.Infrastructure.Data.SocialMedia.SocialModule.Repositories
{
    /// <summary>
    /// The UserProfile repository implementation
    /// </summary>
    public class UserProfileRepository
      : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(ITransactionUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        
        public IQueryable<TEntity> AllMatching<TEntity>(ISpecification<TEntity> specification) 
            where TEntity : UserProfile
        {
            return GetQuery().OfType<TEntity>()
                .Include(u => u.BlockedUsers)
                .Where(specification.SatisfiedBy());
        }

        public IQueryable<TEntity> GetByUserId<TEntity>(string userId)
            where TEntity : UserProfile
        {
            return GetQuery().OfType<TEntity>().Where(u => u.UserId == userId);
        }
    }
        
}
