using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class AddAddressToUserRequest
    {
        public string UserId { get; set; }
        public Dto.Address Address { get; set; }
    }
}
