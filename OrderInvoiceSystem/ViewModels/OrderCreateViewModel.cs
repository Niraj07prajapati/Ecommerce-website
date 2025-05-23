﻿using System.ComponentModel.DataAnnotations;
namespace OrderInvoiceSystem.ViewModels
{
    public class OrderCreateViewModel
    {
   
        [Required]
        public List<OrderItemCreateViewModel> OrderItems { get; set; }
    }

    public class OrderItemCreateViewModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}