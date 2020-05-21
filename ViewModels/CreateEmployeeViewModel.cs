using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace web_hello.ViewModels
{
    public class CreateEmployeeViewModel
    {
        [Required]
        [MaxLength(20, ErrorMessage="Name cannot exceed 20 characters")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage="Invalid email format")]
        [Display(Name="Office Email")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
        public IFormFile Image { get; set; }        
    }
}