using GarageAPI.Common;
using System.ComponentModel;

namespace GarageAPI.ViewModels.Order
{
    public class OrderUpdateDTO
    {
        public Guid Id { get; set; }
        public int OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Not_Started;
        public float Total { get; set; } = 0;
        public float Tax { get; set; } = 0;
        [DisplayName("Sub Total")]
        public float SubTotal { get; set; } = 0;
    }
}
