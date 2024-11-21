using Microsoft.AspNetCore.Mvc;
using GarageAPI.Services.Interfaces;
using GarageAPI.ViewModels.Order;

namespace GarageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            try
            {
                var orders = await _orderService.GetAll();
                if (orders == null)
                {
                    return Ok(new { message = "No orders found" });
                }
                //return Ok(new { message = "Successfully retrieved all blog posts", data = todo });
                return Ok(orders);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving all orders.", error = ex.Message });
            }
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrder(int id)
        {
            try
            {
                var result = await _orderService.GetOrderByID(id);
                if (result == null)
                {
                    return NotFound(new { message = $"No Order with Id {id} found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred while retrieving the Order with Id {id}.", error = ex.Message });
            }
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult> PostOrder(OrderCreateDTO mappedOrder)
        {
            try
            {
                await _orderService.CreateOrder(mappedOrder);
                return Ok(new { message = "Order successfully created" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while creating the Order", error = ex.Message });
            }
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, OrderUpdateDTO order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _orderService.UpdateOrder(id, order);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred while updating cusomer.", error = ex.Message });
            }
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            try
            {
                await _orderService.DeleteOrder(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred while deleting Todo Item  with id {id}", error = ex.Message });
            }
        }
    }
}
