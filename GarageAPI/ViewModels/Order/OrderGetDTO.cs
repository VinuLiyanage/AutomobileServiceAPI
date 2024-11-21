using GarageAPI.Common;
using GarageAPI.ViewModels.Customer;
using GarageAPI.ViewModels.OrdersItem;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GarageAPI.ViewModels.Order
{
    public class OrderGetDTO
    {
        public Guid Id { get; set; }
        public int OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerGetDTO Customer { get; set; } = null!;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Not_Started;
        public float Total { get; set; } = 0;
        public float Tax { get; set; } = 0;

        [DisplayName("Sub Total")]
        public float SubTotal { get; set; } = 0;
        public string CreatedBy { get; set; } = "Admin";

        [DisplayFormat(DataFormatString = "{dd-MM-yyyy HH:mm:ss}")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string LastUpdatedBy { get; set; } = "Admin";

        [DisplayFormat(DataFormatString = "{dd-MM-yyyy HH:mm:ss}")]
        public DateTime LastUpdatedDateTime { get; set; } = DateTime.Now;
        public List<OrdersItemGetDTO> OrdersItem { get; set; } = new List<OrdersItemGetDTO>();
    }
}
