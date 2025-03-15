using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Orders
{
    public class CreateOrderItemDto
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}