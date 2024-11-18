using System.ComponentModel.DataAnnotations;

namespace GarageAPI.Models
{
    public class OrdersItem
    {
        public Guid OrderId { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; } = null!;
        public Order Order { get; set; } = null!;

        [Required]
        public int ItemNo { get; set; }
        public int? Quantity { get; set; }
        [Required]
        public float Total { get; set; }
        public string? Description { get; set; }
        
    }
}
