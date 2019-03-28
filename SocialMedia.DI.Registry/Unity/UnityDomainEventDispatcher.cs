using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Swaksoft.Domain.Seedwork.Events;

namespace SocialMedia.DI.Registry.Unity
{
    public class UnityDomainEventDispatcher : IHandleDomainEvents
    {
        private readonly IUnityContainer container;

        public UnityDomainEventDispatcher(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            this.container = container;
        }

        public void Handle<T>(T domainEvent) where T : IDomainEvent
        {
            var subscribers = container.ResolveAll<IHandle<T>>().ToList();
            subscribers.ForEach(s =>
            {
                try
                {
                    s.Handle(domainEvent);
                }
                finally
                {
                    s?.Dispose();
                }
            });
        }
    }
}