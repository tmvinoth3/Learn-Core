using System.ComponentModel.DataAnnotations;

namespace web_hello.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name="Confirm Password")]
        [Compare("Password", ErrorMessage="Passwod should match.")]
        public string ConfirmPassword { get; set; }
    }
}