using GarageAPI.Common;
using GarageAPI.ViewModels.OrdersItem;
using System.ComponentModel;

namespace GarageAPI.ViewModels.Order
{
    public class OrderCreateDTO
    {
        public Guid CustomerId { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Not_Started;
        public float Total { get; set; } = 0;
        public float Tax { get; set; } = 0;
        [DisplayName("Sub Total")]
        public float SubTotal { get; set; } = 0;
        public List<OrdersItemCreateDTO> OrdersItems { get; set; } = new List<OrdersItemCreateDTO>();
    }
}
