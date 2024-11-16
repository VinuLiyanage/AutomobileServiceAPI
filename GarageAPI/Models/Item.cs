using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageAPI.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public int ItemId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Description { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        [Required]
        public string LastUpdatedBy { get; set; } = "Admin";
        [Required]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy HH:mm:ss}")]
        public DateTime LastUpdatedDateTime { get; set; } = DateTime.Now;
    }
}
