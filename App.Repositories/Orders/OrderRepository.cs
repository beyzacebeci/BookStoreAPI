namespace App.Repositories.Orders;

public class OrderRepository : GenericRepository<Order> , IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
            
        }
    }

