using System.ComponentModel.DataAnnotations;

namespace MyWebApplication.Dtos
{
    public class CreateUserDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
