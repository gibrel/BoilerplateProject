using System.ComponentModel.DataAnnotations;

namespace Boilerplate.API.Models
{
    public class GetUserModel : CreateUserModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}
