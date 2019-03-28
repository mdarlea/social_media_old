using System.Web.Http;
using Microsoft.AspNet.Identity;
using Swaksoft.Core.Dto;

namespace SocialMedia.Host.Controllers
{
    public abstract class ApiControllerBase : ApiController
    {
        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (result.Succeeded) return null;
            if (result.Errors != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            if (ModelState.IsValid)
            {
                // No ModelState errors are available to send, so just return an empty BadRequest.
                return BadRequest();
            }

            return BadRequest(ModelState);
        }

        protected IHttpActionResult GetErrorResult(ActionResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (result.Status == ActionResultCode.Success) return null;
            if (result.Errors != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            if (ModelState.IsValid)
            {
                // No ModelState errors are available to send, so just return an empty BadRequest.
                return BadRequest();
            }

            return BadRequest(ModelState);
        }
    }
}