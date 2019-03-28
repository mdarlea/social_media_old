using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Host.Models.Account
{
    public class CreateExternalApplicationUserModel : CreateUserModel
    {
        [Required]
        [Display(Name = @"First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = @"Last Name")]
        public string LastName { get; set; }

        [Display(Name = @"Skype User")]
        public string SkypeId { get; set; }

        private AddressModel address;
        public AddressModel Address
        {
            get { return address ?? (address = new AddressModel()); }
            set { address = value; }
        }

        [Display(Name = @"Hometown")]
        public string Hometown { get; set; }

       
    }
}