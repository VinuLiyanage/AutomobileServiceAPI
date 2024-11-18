using AutoMapper;
using GarageAPI.Database;
using GarageAPI.Models;
using GarageAPI.Services.Interfaces;
using GarageAPI.ViewModels.Item;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GarageAPI.Services
{
    public class ItemService : IItemService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ItemService> _logger;

        public ItemService(DatabaseContext context, IMapper mapper, ILogger<ItemService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        //Retrieve all items
        public async Task<IEnumerable<ItemGetDTO>> GetAll()
        {
            var items = await _context.Items.ToListAsync();
            if (items == null)
            {
                throw new Exception(" No items found");
            }
            var mappedItem = _mapper.Map<IEnumerable<Item>, IEnumerable<ItemGetDTO>>(items);
            return mappedItem;
        }

        public async Task<ItemGetDTO> GetItemByID(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.ItemId == id);
            if (item == null)
            {
                _logger.LogError(null, $"No item with Id {id} found.");
                throw new KeyNotFoundException($"No item with Id {id} found.");
            }
            var result = _mapper.Map<Item, ItemGetDTO>(item);
            return result;
        }
        public async Task CreateItem(ItemCreateDTO item)
        {
            try
            {
                var mappedItem = _mapper.Map<ItemCreateDTO, Item>(item);
                mappedItem.CreatedBy = "Admin";
                mappedItem.CreatedDateTime = DateTime.Now;
                mappedItem.LastUpdatedBy = "Admin";
                mappedItem.LastUpdatedDateTime = DateTime.Now;

                _context.Items.Add(mappedItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the Item.");
                throw new Exception("An error occurred while creating the Item.", ex);
            }
        }

        public async Task UpdateItem(Guid id, ItemUpdateDTO item)
        {
            try
            {
                var result = await _context.Items.FindAsync(id);
                if (result == null)
                {
                    throw new Exception($"Item with id {id} not found.");
                }
                var mappedItem = _mapper.Map<ItemUpdateDTO, Item>(item);

                PropertyInfo[] contextProp = result.GetType().GetProperties();
                PropertyInfo[] itemDTOProp = item.GetType().GetProperties();

                int i = 0;
                foreach (PropertyInfo property in contextProp)
                {
                    if (itemDTOProp.Length < i + 2) // i dosen't count Id and ItemId. So we add 2
                    {
                        break;
                    }

                    if (property.Name != "Id" && property.Name != "ItemId")
                    {
                        if (property.Name == itemDTOProp[i].Name)
                        {
                            property.SetValue(result, itemDTOProp[i].GetValue(item));
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
                _logger.LogError(ex, $"An error occurred while updating the item with id {id}.");
                throw new Exception($"An error occurred while updating the item. {ex}");
            }
        }

        public async Task DeleteItem(Guid id)
        {
            try
            {
                var item = await _context.Items.FindAsync(id);
                if (item == null)
                {
                    throw new Exception($"No item found with the given id");
                }
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the item.");
                throw new Exception("An error occurred while deleting the item. \n", ex);
            }
        }

        private bool ItemExists(Guid id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
