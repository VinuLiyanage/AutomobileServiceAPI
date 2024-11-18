using GarageAPI.ViewModels.Order;

namespace GarageAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderGetDTO>> GetAll();
        Task<OrderGetDTO> GetOrderByID(int id);
        Task CreateOrder(OrderCreateDTO order);
        Task UpdateOrder(Guid id, OrderUpdateDTO order);
        Task DeleteOrder(Guid id);
    }
}
