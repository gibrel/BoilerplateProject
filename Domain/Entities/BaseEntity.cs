using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
