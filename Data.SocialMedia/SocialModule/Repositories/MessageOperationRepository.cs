using System;
using System.Linq;
using System.Linq.Expressions;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Specification;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.MessageOperationAgg;
using Swaksoft.Infrastructure.Data.Seedwork.Repositories;

namespace Swaksoft.Infrastructure.Data.SocialMedia.SocialModule.Repositories
{
    public class MessageOperationRepository : Repository<MessageOperation>, IMessageOperationRepository
    {
        public MessageOperationRepository(ITransactionUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public IQueryable<TEntity> AllMatching<TEntity>(ISpecification<TEntity> specification) where TEntity : MessageOperation
        {
            return GetQuery().OfType<TEntity>().Where(specification.SatisfiedBy());
        }

        public IQueryable<TEntity> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : MessageOperation
        {
            return GetQuery().OfType<TEntity>().Where(filter);
        }
    }
}
