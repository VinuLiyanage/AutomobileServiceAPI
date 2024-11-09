using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GarageAPI.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [DisplayName("Service")]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
