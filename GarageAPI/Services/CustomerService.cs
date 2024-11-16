using AutoMapper;
using Azure.Core;
using GarageAPI.Database;
using GarageAPI.Models;
using GarageAPI.Services.Interfaces;
using GarageAPI.ViewModels.Customer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;

namespace GarageAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(DatabaseContext context, IMapper mapper, ILogger<CustomerService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        //Retrieve all customers
        public async Task<IEnumerable<CustomerGetDTO>> GetAll()
        {
            var customers = await _context.Customers.ToListAsync();
            if (customers == null)
            {
                throw new Exception(" No Todo items found");
            }
            var mappedCustomer = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerGetDTO>>(customers);
            return mappedCustomer;
        }

        public async Task<CustomerGetDTO> GetCustomerByID(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
            if (customer == null)
            {
                _logger.LogError(null, $"No customer with Id {id} found.");
                throw new KeyNotFoundException($"No customer with Id {id} found.");
            }
            var result = _mapper.Map<Customer, CustomerGetDTO>(customer);
            return result;
        }
        public async Task CreateCustomer(CustomerCreateDTO customer)
        {
            try
            {
                var mappedCustomer = _mapper.Map<CustomerCreateDTO, Customer>(customer);
                mappedCustomer.CreatedBy = "Admin";
                mappedCustomer.CreatedDateTime = DateTime.Now;
                mappedCustomer.LastUpdatedBy = "Admin";
                mappedCustomer.LastUpdatedDateTime = DateTime.Now;

                _context.Customers.Add(mappedCustomer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the customer.");
                throw new Exception("An error occurred while creating the customer.", ex);
            }
        }

        public async Task UpdateCustomer(Guid id, CustomerUpdateDTO customer)
        {               
            try
            {
                var result = await _context.Customers.FindAsync(id);
                if (result == null)
                {
                    throw new Exception($"Customer with id {id} not found.");
                }
                var mappedCustomer = _mapper.Map<CustomerUpdateDTO, Customer>(customer);
                
                PropertyInfo[] contextProp = result.GetType().GetProperties();
                PropertyInfo[] custDTOProp = customer.GetType().GetProperties();

                int i = 0;
                foreach (PropertyInfo property in contextProp)
                {
                    if(custDTOProp.Length < i + 2 ) // i dosen't count Id and CustomerId. So we add 2
                    {
                        break;
                    }

                    if (property.Name != "Id" && property.Name != "CustomerId")
                    {
                        if (property.Name == custDTOProp[i].Name)
                        {
                            property.SetValue(result, custDTOProp[i].GetValue(customer));
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
                _logger.LogError(ex, $"An error occurred while updating the customer with id {id}.");
                throw new Exception($"An error occurred while updating the customer. {ex}");
            }
        }

        public async Task DeleteCustomer(Guid id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                {
                    throw new Exception($"No customer found with the given id");
                }
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the customer.");
                throw new Exception("An error occurred while deleting the customer. \n", ex);
            }
        }

        private bool CustomerExists(Guid id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
