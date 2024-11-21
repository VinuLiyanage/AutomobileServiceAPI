using System.ComponentModel.DataAnnotations;

namespace GarageAPI.ViewModels.OrdersItem
{
    public class OrdersItemGetDTO
    {
        public Guid OrderId { get; set; }
        public Guid ItemId { get; set; }
        public int ItemNo { get; set; }
        public int? Quantity { get; set; }
        public float Total { get; set; }
        public string? Description { get; set; }
        public string CreatedBy { get; set; } = "Admin";

        [DisplayFormat(DataFormatString = "{dd-MM-yyyy HH:mm:ss}")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string LastUpdatedBy { get; set; } = "Admin";

        [DisplayFormat(DataFormatString = "{dd-MM-yyyy HH:mm:ss}")]
        public DateTime LastUpdatedDateTime { get; set; } = DateTime.Now;
    }
}
