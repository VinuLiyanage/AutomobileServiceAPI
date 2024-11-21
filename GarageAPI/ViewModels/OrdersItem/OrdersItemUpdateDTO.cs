namespace GarageAPI.ViewModels.OrdersItem
{
    public class OrdersItemUpdateDTO
    {
        public Guid ItemId { get; set; }
        public int? Quantity { get; set; }
        public float Total { get; set; }
        public string? Description { get; set; } 
    }
}
