using GarageAPI.Models;

namespace GarageAPI.ViewModels.Customer
{
    public class CustomerUpdateDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
