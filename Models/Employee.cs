using System.ComponentModel.DataAnnotations;

namespace web_hello.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage="Name cannot exceed 20 characters")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage="Invalid email format")]
        [Display(Name="Office Email")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
    }
}