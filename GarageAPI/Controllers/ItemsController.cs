using Microsoft.AspNetCore.Mvc;
using GarageAPI.ViewModels.Item;
using GarageAPI.Services.Interfaces;

namespace GarageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult> GetItems()
        {
            try
            {
                var items = await _itemService.GetAll();
                if (items == null)
                {
                    return Ok(new { message = "No items found" });
                }
                //return Ok(new { message = "Successfully retrieved all blog posts", data = todo });
                return Ok(items);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving all items.", error = ex.Message });
            }
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetItem(int id)
        {
            try
            {
                var result = await _itemService.GetItemByID(id);
                if (result == null)
                {
                    return NotFound(new { message = $"No Item with Id {id} found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred while retrieving the Item with Id {id}.", error = ex.Message });
            }
        }

        // POST: api/Items
        [HttpPost]
        public async Task<ActionResult> PostItem(ItemCreateDTO mappedItem)
        {
            try
            {
                await _itemService.CreateItem(mappedItem);
                return Ok(new { message = "Item successfully created" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while creating the Item", error = ex.Message });
            }
        }

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(Guid id, ItemUpdateDTO item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _itemService.UpdateItem(id, item);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred while updating cusomer.", error = ex.Message });
            }
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            try
            {
                await _itemService.DeleteItem(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred while deleting Todo Item  with id {id}", error = ex.Message });
            }
        }
    }
}
