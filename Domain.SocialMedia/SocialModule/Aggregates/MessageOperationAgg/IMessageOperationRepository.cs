using System;
using System.Linq;
using System.Linq.Expressions;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.Seedwork.Specification;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.MessageOperationAgg
{
    public interface IMessageOperationRepository : IRepository<MessageOperation>
    {
        IQueryable<TEntity> AllMatching<TEntity>(ISpecification<TEntity> specification)
            where TEntity : MessageOperation;

        IQueryable<TEntity> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : MessageOperation;
    }
}
