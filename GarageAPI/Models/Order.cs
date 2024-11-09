﻿using GarageAPI.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageAPI.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId {  get; set; }

        [Required]
        public Customer Customer { get; set; } = new Customer();

        [Required]
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Not_Started;

        [Required]
        public float Total {  get; set; } = 0;

        [Required]
        public float Tax { get; set; } = 0;

        [Required]
        [DisplayName("Sub Total")]
        public float SubTotal { get; set; } = 0;
    }
}