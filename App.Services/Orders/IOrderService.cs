using App.Services.Books;

namespace App.Services.Orders;

public interface IOrderService
{
    Task<ServiceResult<OrderDto?>> GetByIdAsync(int id);
    Task<ServiceResult<CreateOrderResponseDto>> CreateAsync(CreateOrderRequestDto requestDto);
    Task<ServiceResult> UpdateStatusAsync(int id, UpdateOrderStatusRequestDto requestDto);
}

