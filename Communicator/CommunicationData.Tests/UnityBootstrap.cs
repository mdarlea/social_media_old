using System;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.VerificationCodeAgg;
using Swaksoft.Infrastructure.Data.Communicator.MessagingModule.Repositories;
using Microsoft.Practices.Unity;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Infrastructure.Data.Communicator.Tests
{
    public class UnityBootstrap
    {
        private readonly IUnityContainer _container;

        public UnityBootstrap(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            _container = container;
            Register();
        }

        public void Register()
        {
            _container.RegisterType<ITransactionUnitOfWork, ProfiledCommunicatorUnitOfWork>(new PerResolveLifetimeManager());
            RegisterRepositories();
        }

        private void RegisterRepositories()
        {
            _container.RegisterType<IRepository<VerificationCode>, VerificationCodeRepository>();

            _container.RegisterType<IRepository<CommunicationLog>, CommunicationLogRepository>();

            _container.RegisterType<IRepository<VoiceOperation>, VoiceOperationRepository>();
            _container.RegisterType<IRepository<SmsOperation>, SmsOperationRepository>();
            _container.RegisterType<IRepository<CommunicatorProfile>, CommunicatorProfileRepository>();
        }
    }
}
