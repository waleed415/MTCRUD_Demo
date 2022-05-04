using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MTCRUD.Models
{
    public class Order
    {
        public Order()
        {
            Details = new List<OrderDetail>();
        }
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }

        public List<OrderDetail> Details { get; set; }
    }
}
