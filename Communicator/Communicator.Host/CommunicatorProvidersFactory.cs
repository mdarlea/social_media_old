using System;
using AntiCorruption.Twillio;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Aggregates.ProfileAgg;
using IProvider = Swaksoft.Domain.Communicator.MessagingModule.Providers.IProvider;

namespace Swaksoft.Communicator.Host
{
    public class CommunicatorProvidersFactory : ActionsFactory<Profile, IProvider>
    {
        public CommunicatorProvidersFactory()
        {
            //ToDo: scan types in assembly
            Initialize<TwilioProfile>(profile => new TwillioCommunicatorProvider(profile));
        }
    }
}
