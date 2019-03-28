using System;
using System.Collections.Generic;
using Swaksoft.Domain.Seedwork.Events;

namespace Application.SocialMedia.Tests
{
    public class TestDomainEventsHandlers : IHandleDomainEvents
    {
        readonly List<IDomainEvent> _raisedDomainEvents = new List<IDomainEvent>();

        public IEnumerable<IDomainEvent> RaisedDomainEvents { get { return _raisedDomainEvents; } }

        public void Handle<T>(T domainEvent) 
            where T : IDomainEvent
        {
            _raisedDomainEvents.Add(domainEvent);
        }
    }
}
