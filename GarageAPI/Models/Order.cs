using GarageAPI.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageAPI.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId {  get; set; }
        [Required]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        [Required]
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Not_Started;
        [Required]
        public float Total {  get; set; } = 0;
        [Required]
        public float Tax { get; set; } = 0;
        [Required]
        [DisplayName("Sub Total")]
        public float SubTotal { get; set; } = 0;
        [Required]
        public string CreatedBy { get; set; } = "Admin";
        [Required]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy HH:mm:ss}")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        [Required]
        public string LastUpdatedBy { get; set; } = "Admin";
        [Required]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy HH:mm:ss}")]
        public DateTime LastUpdatedDateTime { get; set; } = DateTime.Now;
        public ICollection<OrdersItem> OrdersItem { get; set; } // Navigation property for the many-to-many relationship
    }
}
