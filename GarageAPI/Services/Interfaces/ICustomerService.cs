using GarageAPI.ViewModels.Customer;

namespace GarageAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerGetDTO>> GetAll();
        Task<CustomerGetDTO> GetCustomerByID(int id);
        Task CreateCustomer(CustomerCreateDTO customer);
        Task UpdateCustomer(Guid id, CustomerUpdateDTO customer);
        Task DeleteCustomer(Guid id);
    }
}
