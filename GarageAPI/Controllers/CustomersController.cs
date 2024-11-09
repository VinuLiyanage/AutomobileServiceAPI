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

namespace GarageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public CustomersController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerGetDTO>>> GetCustomers()
        {
            var result = await _context.Customers.ToListAsync();
            List<CustomerGetDTO> mappedCustomer = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerGetDTO>>(result).ToList();
            return mappedCustomer;
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerUpdateDTO>> GetCustomer(Guid id)
        {
            var result = await _context.Customers.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            var customer = _mapper.Map<Customer, CustomerUpdateDTO>(result);
            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, CustomerUpdateDTO mappedCustomer)
        {
            var customer = _mapper.Map<CustomerUpdateDTO, Customer>(mappedCustomer);

            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerCreateDTO>> PostCustomer(CustomerCreateDTO mappedCustomer)
        {
            var customer = _mapper.Map<CustomerCreateDTO, Customer>(mappedCustomer);

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(Guid id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
