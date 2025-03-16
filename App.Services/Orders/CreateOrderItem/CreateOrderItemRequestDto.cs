using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Orders.CreateOrderItem
{
    public class CreateOrderItemRequestDto
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}