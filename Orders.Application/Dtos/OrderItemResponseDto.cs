using Orders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.Application.Dtos
{
    public class OrderItemResponsetDto
    {
        public Guid ProductId { get; set; }
        public string name { get; set; }
        public int Quantity { get; set; }
    }
}
