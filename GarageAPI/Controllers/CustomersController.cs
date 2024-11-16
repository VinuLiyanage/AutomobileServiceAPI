using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GarageAPI.Database;
using GarageAPI.Models;
using AutoMapper;
using GarageAPI.ViewModels.Customer;
using Microsoft.AspNetCore.Http.HttpResults;
using GarageAPI.Services.Interfaces;
using NuGet.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Azure.Core;

namespace GarageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult> GetCustomers()
        {
            try
            {
                var customers = await _customerService.GetAll();
                if (customers == null)
                {
                    return Ok(new { message = "No customers found" });
                }
                //return Ok(new { message = "Successfully retrieved all blog posts", data = todo });
                return Ok(customers);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving all customers.", error = ex.Message });
            }
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomer(int id)
        {
            try
            {
                var result = await _customerService.GetCustomerByID(id);
                if (result == null)
                {
                    return NotFound(new { message = $"No Customer with Id {id} found." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred while retrieving the Customer with Id {id}.", error = ex.Message });
            }
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult> PostCustomer(CustomerCreateDTO mappedCustomer)
        {
            try
            {
                await _customerService.CreateCustomer(mappedCustomer);
                return Ok(new { message = "Customer successfully created" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while creating the Customer", error = ex.Message });
            }
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, CustomerUpdateDTO customer)
        {           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _customerService.UpdateCustomer(id, customer);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred while updating cusomer.", error = ex.Message });
            }
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            try
            {
                await _customerService.DeleteCustomer(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"An error occurred while deleting Todo Item  with id {id}", error = ex.Message });
            }
        }       
    }
}
