namespace App.Repositories.Orders;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order?> GetOrderWithDetailsAsync(int id);
}

