using System;
using System.Collections.Generic;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Contracts.Streaming
{
    public interface IClientsRegistry
    {
        void AddClient(IClient client);
        IClient FindClient(Func<IClient, bool> predicate);
        IEnumerable<IClient> GetAllClients();
        IEnumerable<IClient> GetActiveClients();
    }
}