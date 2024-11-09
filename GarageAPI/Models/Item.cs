using System.ComponentModel.DataAnnotations;

namespace GarageAPI.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Description { get; set; }
    }
}
