namespace GarageAPI.ViewModels.Item
{
    public class ItemUpdateDTO
    {
        public Guid Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
