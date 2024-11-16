using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageAPI.Models
{
    public class Service
    {
        public Guid Id { get; set; }
        public int ServiceId { get; set; }

        [Required]
        [MaxLength(20)]
        [DisplayName("Service")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        [Required]
        public string LastUpdatedBy { get; set; } = "Admin";
        [Required]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy HH:mm:ss}")]
        public DateTime LastUpdatedDateTime { get; set; } = DateTime.Now;
    }
}
