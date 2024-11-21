using GarageAPI.Common;
using GarageAPI.ViewModels.OrdersItem;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GarageAPI.ViewModels.Order
{
    public class OrderUpdateDTO
    {
        public Guid CustomerId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public float Total { get; set; } = 0;
        public float Tax { get; set; } = 0;
        [DisplayName("Sub Total")]
        public float SubTotal { get; set; } = 0;
        public ICollection<OrdersItemUpdateDTO> OrdersItem { get; set; } = new List<OrdersItemUpdateDTO>();
    }
}
