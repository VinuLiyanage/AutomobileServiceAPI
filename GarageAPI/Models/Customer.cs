﻿using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageAPI.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required]
        [DisplayName("First Name")]
        [MaxLength(20)]
        public string FirstName { get; set; } = String.Empty;

        [Required]
        [DisplayName("Last Name")]
        [MaxLength(20)]
        public string LastName { get; set; } = String.Empty;

        [DisplayName("Address Line 1")]
        [MaxLength(30)]
        public string? AddressLine1 { get; set; }

        [DisplayName("Address Line 2")]
        [MaxLength(30)]
        public string? AddressLine2 { get; set; }

        [DisplayName("Address Line 3")]
        [MaxLength(30)]
        public string? AddressLine3 { get; set; }

        [MaxLength(20)]
        public string? City { get; set; }

        [DisplayName("State/Province")]
        [MaxLength(15)]
        public string? State { get; set; }

        [DisplayName("Postal Code")]
        [MaxLength(10)]
        public string? PostalCode { get; set; }

        [Required]
        [StringLength(14)]
        [RegularExpression(@"\(\d{3}\) \d{3}-\d{4}")]
        public string PhoneNumber { get; set; } = String.Empty;
        [Required]
        public string CreatedBy { get; set; } = "Admin";
        [Required]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy HH:mm:ss}")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        [Required]
        public string LastUpdatedBy { get; set; } = "Admin";
        [Required]
        [DisplayFormat(DataFormatString = "{dd-MM-yyyy HH:mm:ss}")]
        public DateTime LastUpdatedDateTime { get; set; } = DateTime.Now;
        public ICollection<Order>? Orders { get; set; } // Collection navigation containing dependents
    }
}