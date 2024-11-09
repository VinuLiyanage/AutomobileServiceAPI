using GarageAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GarageAPI.ViewModels.Customer
{
    public class CustomerGetDTO
    {
        public Guid Id { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}
