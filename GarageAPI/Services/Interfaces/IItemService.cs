using GarageAPI.ViewModels.Customer;
using GarageAPI.ViewModels.Item;

namespace GarageAPI.Services.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<ItemGetDTO>> GetAll();
        Task<ItemGetDTO> GetItemByID(int id);
        Task CreateItem(ItemCreateDTO item);
        Task UpdateItem(Guid id, ItemUpdateDTO item);
        Task DeleteItem(Guid id);
    }
}
