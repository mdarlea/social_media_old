using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Contracts.Streaming
{
    public class ClientsRegistry : IClientsRegistry
    {
        private static readonly ConcurrentBag<IClient> activeClients = new ConcurrentBag<IClient>();

        public void AddClient(IClient client)
        {
            activeClients.Add(client);
        }

        public IClient FindClient(Func<IClient, bool> predicate)
        {
            return activeClients.FirstOrDefault(predicate);
        }

        public IEnumerable<IClient> GetAllClients()
        {
            return from c in activeClients select c;
        }

        public IEnumerable<IClient> GetActiveClients()
        {
            return activeClients.Where(c => c.ClientState != ClientState.Stopped);
        } 
    }
}
