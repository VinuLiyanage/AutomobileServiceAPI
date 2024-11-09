using AutoMapper;
using GarageAPI.ViewModels.Customer;
using GarageAPI.Models;
using GarageAPI.ViewModels.Item;

namespace GarageAPI.Map
{
    public class MappingModel : Profile
    {
        public MappingModel()
        {
            //Customer 
            CreateMap<CustomerCreateDTO, Customer>();
            CreateMap<CustomerUpdateDTO, Customer>();
            CreateMap<Customer, CustomerGetDTO>();

            //Item
            CreateMap<ItemCreateDTO, Item>();
            CreateMap<ItemUpdateDTO, Item>();
            CreateMap<Item, ItemGetDTO>();
        }
    }
}
