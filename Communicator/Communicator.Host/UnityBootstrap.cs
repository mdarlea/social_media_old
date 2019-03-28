using System;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Application.Communicator.MessagingModule.Services;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicationLogAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorPhoneNumberAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.MessageOperationAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.VerificationCodeAgg;
using Swaksoft.Domain.Communicator.MessagingModule.Handlers;
using Swaksoft.Communicator.Host.Controllers;
using Swaksoft.Infrastructure.Data.Communicator.MessagingModule.Repositories;
using Swaksoft.Infrastructure.Data.Communicator.UnitOfWork;
using Microsoft.Practices.Unity;
using Swaksoft.Application.Communicator.MessagingModule.Services.Providers.Twilio;
using Swaksoft.Domain.Communicator.MessagingModule.Providers;
using Swaksoft.Domain.Communicator.MessagingModule.Services;
using Swaksoft.Domain.Communicator.MessagingModule.Services.Providers.Twilio;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.Seedwork.Aggregates.ProfileAgg;
using Swaksoft.Domain.Seedwork.Events;
using IProvider = Swaksoft.Domain.Communicator.MessagingModule.Providers.IProvider;

namespace Swaksoft.Communicator.Host
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
            _container.RegisterType<ITransactionUnitOfWork, CommunicatorUnitOfWork>(new PerResolveLifetimeManager());

            RegisterRepositories();
            RegisterDomainServices();
            RegisterAppServices();

            RegisterSingletons();

            RegisterDomainEventsHandlers();
        }

        private void RegisterSingletons()
        {
            _container.RegisterType<ActionsFactory<Profile, IProvider>, CommunicatorProvidersFactory>(
                new ContainerControlledLifetimeManager());
        }

        private static void RegisterDomainEventsHandlers()
        {
            DomainEvents.Register(new VerificationCodeSentHandler());
        }

        private void RegisterRepositories()
        {
            _container.RegisterType<IRepository<VerificationCode>, VerificationCodeRepository>();

            _container.RegisterType<IRepository<CommunicationLog>, CommunicationLogRepository>();

            _container.RegisterType<IRepository<VoiceOperation>, VoiceOperationRepository>();
            _container.RegisterType<IRepository<SmsOperation>, SmsOperationRepository>();

            _container.RegisterType<ICommunicatorPhoneNumberRepository, CommunicatorPhoneNumberRepository>();

            _container.RegisterType<IRepository<CommunicatorProfile>, CommunicatorProfileRepository>();
        }

        private void RegisterAppServices()
        {
            _container.RegisterType<ICommunicatorAppService, OutboundCallAppService>("OutboundCallAppService");
            _container.RegisterType<ICommunicatorAppService, SmsAppService>("SmsAppService");

            _container.RegisterType<OutboundCallController>(
                new InjectionConstructor(new ResolvedParameter<ICommunicatorAppService>("OutboundCallAppService")));
            _container.RegisterType<SmsController>(
                new InjectionConstructor(new ResolvedParameter<ICommunicatorAppService>("SmsAppService")));

            RegisterThirdPartyAppServices();
        }

        private void RegisterThirdPartyAppServices()
        {
            _container.RegisterType<IXmlVoiceMessagingAppService, TwimlVoiceMessagingAppService>();
        }

        private void RegisterDomainServices()
        {
            _container.RegisterType<ICommunicatorProviderFactory, CommunicatorProviderFactory>();
            _container.RegisterType<IVoiceOperationsService, VoiceOperationsService>();
        }
    }
}