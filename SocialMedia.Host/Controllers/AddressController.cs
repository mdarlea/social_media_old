using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Swaksoft.Application.SocialMedia.Dto;
using Swaksoft.Application.SocialMedia.SocialModule.Services;
using Swaksoft.Core.Dto;

namespace SocialMedia.Host.Controllers
{
    [RoutePrefix("api/address")]
    [Authorize]
    public class AddressController : ApiControllerBase
    {
        private readonly IAddressAppService addressAppService;

        public AddressController(IAddressAppService addressAppService)
        {
            if (addressAppService == null) throw new ArgumentNullException(nameof(addressAppService));
            this.addressAppService = addressAppService;
        }

        [HttpGet]
        [Route("FindAddressesForUser")]
        public IHttpActionResult FindAddressesForUser()
        {
            var userId = User.Identity.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new AuthenticationException($"Could not authenticate {userId}");
            }

            var result = addressAppService.FindAddressesForUser(userId);

            return result.Status != ActionResultCode.Success 
                ? GetErrorResult(result) 
                : Ok(result.Items);
        }

        [HttpGet]
        [Route("FindAddressById/{id}")]
        public IHttpActionResult FindAddressById(int id)
        {
            var result = addressAppService.FindAddressById(id);

            return result.Status != ActionResultCode.Success 
                ? GetErrorResult(result) 
                : Ok(result);
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                addressAppService.Dispose();
            }
        }
    }
}
