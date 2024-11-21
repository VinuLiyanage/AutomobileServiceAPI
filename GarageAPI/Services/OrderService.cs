using AutoMapper;
using GarageAPI.Database;
using GarageAPI.Models;
using GarageAPI.Services.Interfaces;
using GarageAPI.ViewModels.Item;
using GarageAPI.ViewModels.Order;
using GarageAPI.ViewModels.OrdersItem;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

namespace GarageAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(DatabaseContext context, IMapper mapper, ILogger<OrderService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        //Retrieve all orders
        public async Task<IEnumerable<OrderGetDTO>> GetAll()
        {
            var orders = await _context.Orders.Include(x => x.OrdersItem).ToListAsync();

            if (orders == null)
            {
                throw new Exception("No orders found");
            }
            var mappedOrder = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderGetDTO>>(orders);

            return mappedOrder;
        }

        public async Task<OrderGetDTO> GetOrderByID(int id)
        {
            var order = await _context.Orders.Include(x => x.OrdersItem).FirstOrDefaultAsync(x => x.OrderId == id);
            if (order == null)
            {
                _logger.LogError(null, $"No order with Id {id} found.");
                throw new KeyNotFoundException($"No order with Id {id} found.");
            }
            var result = _mapper.Map<Order, OrderGetDTO>(order);
            return result;
        }
        public async Task CreateOrder(OrderCreateDTO order)
        {
            try
            {
                var mappedOrder = _mapper.Map<OrderCreateDTO, Order>(order);
                
                mappedOrder.Id = Guid.NewGuid();
                mappedOrder.CreatedBy = "Admin";
                mappedOrder.CreatedDateTime = DateTime.Now;
                mappedOrder.LastUpdatedBy = "Admin";
                mappedOrder.LastUpdatedDateTime = DateTime.Now;

                //List<OrdersItem> ordersItems = new List<OrdersItem>();
                var mappedOrderItem = _mapper.Map<List<OrdersItemCreateDTO>, List<OrdersItem>>(order.OrdersItems);
                int itemNo = 1;
                foreach (var mappeditem in mappedOrderItem)
                {
                    mappeditem.OrderId = mappedOrder.Id;
                    mappeditem.ItemNo = itemNo;
                    mappeditem.CreatedBy = "Admin";
                    mappeditem.CreatedDateTime = DateTime.Now;
                    mappeditem.LastUpdatedBy = "Admin";
                    mappeditem.LastUpdatedDateTime = DateTime.Now;

                    //OrdersItem item = new OrdersItem();
                    //item.OrderId = mappeditem.OrderId;
                    //item.ItemId = mappeditem.ItemId;
                    //item.ItemNo = itemNo;
                    //item.Quantity = mappeditem.Quantity;
                    //item.Total = mappeditem.Total;
                    //item.Description = mappeditem.Description;
             
                    //ordersItems.Add(item);
                    
                    _context.OrdersItems.Add(mappeditem);

                    itemNo++;
                }
                _context.Orders.Add(mappedOrder);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the order.");
                throw new Exception("An error occurred while creating the order.", ex);
            }
        }

        public async Task UpdateOrder(Guid id, OrderUpdateDTO order)
        {
            try
            {
                var result = await _context.Orders.Include(x => x.OrdersItem).FirstOrDefaultAsync(x => x.Id == id);
                if (result == null)
                {
                    throw new Exception($"Order with id {id} not found.");
                }
                var mappedOrder = _mapper.Map<OrderUpdateDTO, Order>(order);

                PropertyInfo[] contextProp = result.GetType().GetProperties();
                PropertyInfo[] custDTOProp = order.GetType().GetProperties();

                int i = 0;
                foreach (PropertyInfo property in contextProp)
                {
                    if (custDTOProp.Length < i + 2) // i dosen't count Id and OrderId. So we add 2
                    {
                        break;
                    }

                    if (property.Name != "Id" && property.Name != "OrderId")
                    {
                        if (property.Name == custDTOProp[i].Name)
                        {
                            property.SetValue(result, custDTOProp[i].GetValue(order));
                            i++;
                        }
                        else
                        {
                            _logger.LogError(null, "Property names are not matched when assigning values");
                            throw new Exception($"Property names are not matched when assigning values.");
                        }
                    }

                }

                result.LastUpdatedBy = "Admin";
                result.LastUpdatedDateTime = DateTime.Now;


                await _context.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the order with id {id}.");
                throw new Exception($"An error occurred while updating the order. {ex}");
            }
        }

        public async Task DeleteOrder(Guid id)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);
                if (order == null)
                {
                    throw new Exception($"No order found with the given id");
                }
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the order.");
                throw new Exception("An error occurred while deleting the order. \n", ex);
            }
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
