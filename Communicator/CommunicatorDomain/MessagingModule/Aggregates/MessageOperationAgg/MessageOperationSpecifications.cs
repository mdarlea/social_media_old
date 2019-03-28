using Swaksoft.Domain.Seedwork.Specification;

namespace Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg
{
    public static class MessageOperationSpecifications
    {
        public static ISpecification<T> VerificationCodeAction<T>()
            where T : MessageOperation
        {
            return new DirectSpecification<T>(vo => vo.Action == "VerificationCode");
        }

        public static ISpecification<T> SayWordAction<T>()
            where T : MessageOperation
        {
            return new DirectSpecification<T>(vo => vo.Action == "SayWord");
        } 
    }
}
