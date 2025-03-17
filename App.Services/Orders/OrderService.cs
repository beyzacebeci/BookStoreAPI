using App.Repositories;
using App.Repositories.Books;
using App.Repositories.OrderItems;
using App.Repositories.Orders;
using App.Services.Orders.Create;
using App.Services.Orders.UpdateStatus;
using AutoMapper;
using System.Net;

namespace App.Services.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public OrderService(
        IOrderRepository orderRepository,
        IBookRepository bookRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ServiceResult<OrderDto?>> GetByIdAsync(int id)
    {
        var order = await _orderRepository.GetOrderWithDetailsAsync(id);

        if (order is null)
        {
            return ServiceResult<OrderDto?>.Fail("Order is not found.", HttpStatusCode.NotFound);
        }

        var orderDto = _mapper.Map<OrderDto>(order);


        return ServiceResult<OrderDto>.Success(orderDto)!;
    }

    public async Task<ServiceResult<CreateOrderResponseDto>> CreateAsync(CreateOrderRequestDto requestDto)
    {
        // Order validation
        if (requestDto.OrderItems == null || !requestDto.OrderItems.Any())
        {
            return ServiceResult<CreateOrderResponseDto>.Fail(
                "An order must contain at least one product.",
                HttpStatusCode.BadRequest);
        }

        decimal totalPrice = 0;
        var orderItems = new List<OrderItem>();

        // Stock control for each book and creating an OrderItem.
        foreach (var item in requestDto.OrderItems)
        {
            var book = await _bookRepository.GetByIdAsync(item.BookId);

            if (book == null)
            {
                return ServiceResult<CreateOrderResponseDto>.Fail(
                    $"Book not found. ID: {item.BookId}",
                    HttpStatusCode.NotFound);
            }

            // Stock control
            if (book.StockQuantity < item.Quantity)
            {
                return ServiceResult<CreateOrderResponseDto>.Fail(
                    $"Insufficient stock. Book: {book.Title}, Current stock: {book.StockQuantity}, Requested: {item.Quantity}",
                    HttpStatusCode.BadRequest);
            }

            var orderItem = new OrderItem
            {
                BookId = book.Id,
                Quantity = item.Quantity,
                UnitPrice = book.Price
            };

            orderItems.Add(orderItem);
            totalPrice += book.Price * item.Quantity;

            // update stock quantity
            book.StockQuantity -= item.Quantity;
            _bookRepository.Update(book);
        }

        // Create Order
        var order = new Order
        {
            OrderDate = DateTime.UtcNow,
            TotalPrice = totalPrice,
            Status = Order.OrderStatus.PENDING,
            OrderItems = orderItems
        };

        await _orderRepository.AddAsync(order); 
        await _unitOfWork.SaveChangesAsync();

        var responseDto = _mapper.Map<CreateOrderResponseDto>(order);
        return ServiceResult<CreateOrderResponseDto>.Success( responseDto,HttpStatusCode.Created);
    }

    public async Task<ServiceResult> UpdateStatusAsync(int id, UpdateOrderStatusRequestDto requestDto)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order is null)
        {
            return ServiceResult.Fail("Order not found.", HttpStatusCode.NotFound);
        }

        // Convert status string to enum
        if (!Enum.TryParse<Order.OrderStatus>(requestDto.Status, true, out var newStatus))
        {
            return ServiceResult.Fail(
                "Invalid order status. Valid values: PENDING, COMPLETED, CANCELLED.",
                HttpStatusCode.BadRequest);
        }

        // COMPLETED siparişler için güncelleme yapılamaz
        if (order.Status == Order.OrderStatus.COMPLETED)
        {
            return ServiceResult.Fail(
                "The status of completed orders cannot be changed.",
                HttpStatusCode.BadRequest);
        }

        // Sipariş iptal ediliyorsa stok miktarını geri ekle
        if (newStatus == Order.OrderStatus.CANCELLED && order.Status != Order.OrderStatus.CANCELLED)
        {
            var orderItems = await _orderRepository.GetOrderWithDetailsAsync(id);
            foreach (var item in orderItems!.OrderItems)
            {
                var book = await _bookRepository.GetByIdAsync(item.BookId);
                if (book != null)
                {
                    book.StockQuantity += item.Quantity;
                    _bookRepository.Update(book);
                }
            }
        }

        order.Status = newStatus;
        await _unitOfWork.SaveChangesAsync();
        return ServiceResult.Success();
    }
}