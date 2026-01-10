using System.ComponentModel.DataAnnotations;

namespace MyWebApplication.Dtos
{
    public class UpdateUserDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [EmailAddress] 
        public string Email { get; set;}

        public bool IsActive { get; set;}
    }
}
