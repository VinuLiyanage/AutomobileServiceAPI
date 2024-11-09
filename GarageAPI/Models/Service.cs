using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageAPI.Models
{
    public class Service
    {
        public Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }

        [Required]
        [MaxLength(20)]
        [DisplayName("Service")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
