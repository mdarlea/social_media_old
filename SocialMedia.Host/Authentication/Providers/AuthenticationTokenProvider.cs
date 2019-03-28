using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Infrastructure;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Token;

namespace SocialMedia.Host.Authentication.Providers
{
    public class AuthenticationTokenProvider : IAuthenticationTokenProvider
    {
        private readonly IAuthenticationTokenFactory authenticationTokenFactory;

        public AuthenticationTokenProvider(IAuthenticationTokenFactory authenticationTokenFactory)
        {
            if (authenticationTokenFactory == null) throw new ArgumentNullException(nameof(authenticationTokenFactory));
            this.authenticationTokenFactory = authenticationTokenFactory;
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
                var refreshToken = await authenticationTokenFactory.CreateRefreshTokenAsync(
                    context.Ticket, context.SerializeTicket());

                if (!string.IsNullOrWhiteSpace(refreshToken))
                {
                    context.SetToken(refreshToken);    
                }
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            
            //var hashedTokenId = CryptoAes.GetHash(context.Token);
            var ticket = await authenticationTokenFactory.IssueAuthenticationTicketAsync(context.Token);
            if (ticket != null)
            {
                context.SetTicket(ticket);    
            }
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}