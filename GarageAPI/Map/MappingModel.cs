using AutoMapper;
using GarageAPI.ViewModels.Customer;
using GarageAPI.Models;
using GarageAPI.ViewModels.Item;
using GarageAPI.ViewModels.Order;
using GarageAPI.ViewModels.OrdersItem;

namespace GarageAPI.Map
{
    public class MappingModel : Profile
    {
        public MappingModel()
        {
            //Customer 
            CreateMap<CustomerCreateDTO, Customer>()
                .ForMember(cust => cust.Id, opt => opt.Ignore())
                .ForMember(cust => cust.CustomerId, opt => opt.Ignore());

            CreateMap<CustomerUpdateDTO, Customer>()
                .ForMember(cust => cust.Id, opt => opt.Ignore())
                .ForMember(cust => cust.CustomerId, opt => opt.Ignore());

            CreateMap<Customer, CustomerGetDTO>();

            //Item
            CreateMap<ItemCreateDTO, Item>()
                .ForMember(item => item.Id, opt => opt.Ignore())
                .ForMember(item => item.ItemId, opt => opt.Ignore());

            CreateMap<ItemUpdateDTO, Item>()
                .ForMember(item => item.Id, opt => opt.Ignore())
                .ForMember(item => item.ItemId, opt => opt.Ignore());

            CreateMap<Item, ItemGetDTO>();

            //Order
            CreateMap<OrderCreateDTO, Order>()
                .ForMember(item => item.OrderId, opt => opt.Ignore());

            CreateMap<OrderUpdateDTO, Order>()
                .ForMember(item => item.OrderId, opt => opt.Ignore());

            CreateMap<Order, OrderGetDTO>();

            //Orders Item
            CreateMap<OrdersItemCreateDTO, OrdersItem>()
                .ForMember(item => item.OrderId, opt => opt.Ignore());
            CreateMap<OrdersItemUpdateDTO, OrdersItem>();
            CreateMap<OrdersItem, OrdersItemGetDTO>();
        }
    }
}
