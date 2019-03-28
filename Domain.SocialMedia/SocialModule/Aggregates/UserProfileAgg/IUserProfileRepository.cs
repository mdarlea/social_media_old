using System;
using System.Linq;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.Seedwork.Specification;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg
{
    /// <summary>
    /// UserProfile repository contract
    /// </summary>
    public interface IUserProfileRepository
        : IRepository<UserProfile>
    {
        IQueryable<TEntity> AllMatching<TEntity>(ISpecification<TEntity> specification) 
            where TEntity:UserProfile;

        IQueryable<TEntity> GetByUserId<TEntity>(string userId)
            where TEntity : UserProfile;
    }
}
