namespace GarageAPI.ViewModels.Item
{
    public class ItemCreateDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsService { get; set; } = false;// True if it's a service
    }
}
