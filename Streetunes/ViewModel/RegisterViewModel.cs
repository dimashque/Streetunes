using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Streetunes.ViewModel
{
    public class RegisterViewModel
    {
        [Display(Name = "Artist or Band Name")]
        [Required(ErrorMessage = "Email Address is required!")]
        

        public string UserName { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is required!")]

        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required!")]
        [Compare("Password", ErrorMessage = "Passwrods do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}