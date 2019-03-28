using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Host.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The client id is required")]
        public string ClientId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}