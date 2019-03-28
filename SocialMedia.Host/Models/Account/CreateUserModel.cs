using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Host.Models.Account
{
    public class CreateUserModel
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = @"Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = @"User Name")]
        public string UserName { get; set; }

        //[Required]
        public string Provider { get; set; }
    }
}