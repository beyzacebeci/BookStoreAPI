using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Repositories.Books;

namespace App.Repositories.Orders;

public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    // Foreign keys
    public int OrderId { get; set; }
    public int BookId { get; set; }

    // Navigation properties
    public Order Order { get; set; } = default!;
    public Book Book { get; set; } = default!;
}

