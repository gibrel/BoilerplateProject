using System.ComponentModel.DataAnnotations;

namespace Boilerplate.API.Models
{
    public class CreateUserModel
    {
        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(64)]
        public string PasswordHash { get; set; }
    }
}
