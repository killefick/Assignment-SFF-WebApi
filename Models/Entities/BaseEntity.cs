using System.ComponentModel.DataAnnotations;

namespace SFF.Models
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}