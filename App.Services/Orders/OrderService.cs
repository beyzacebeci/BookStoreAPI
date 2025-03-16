using App.Repositories;
using App.Repositories.Books;
using App.Repositories.OrderItems;
using App.Repositories.Orders;
using App.Services.Orders.Create;
using App.Services.Orders.UpdateStatus;
using System.Net;

namespace App.Services.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(
        IOrderRepository orderRepository,
        IBookRepository bookRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResult<OrderDto?>> GetByIdAsync(int id)
    {
        var order = await _orderRepository.GetOrderWithDetailsAsync(id);

        if (order is null)
        {
            return ServiceResult<OrderDto?>.Fail("Order is not found.", HttpStatusCode.NotFound);
        }

        var orderDto = new OrderDto
        {
            Id = order.Id,
            TotalPrice = order.TotalPrice,
            OrderDate = order.OrderDate,
            Status = order.Status.ToString(),
            Items = order.OrderItems.Select(item => new OrderItemDto
            {
                BookId = item.BookId,
                BookTitle = item.Book.Title,
                BookAuthor = item.Book.Author,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                SubTotal = item.UnitPrice * item.Quantity
            }).ToList()
        };

        return ServiceResult<OrderDto>.Success(orderDto)!;
    }

    public async Task<ServiceResult<CreateOrderResponseDto>> CreateAsync(CreateOrderRequestDto requestDto)
    {
        // 1. Sipariş validasyonu
        if (requestDto.OrderItems == null || !requestDto.OrderItems.Any())
        {
            return ServiceResult<CreateOrderResponseDto>.Fail(
                "An order must contain at least one product.",
                HttpStatusCode.BadRequest);
        }

        decimal totalPrice = 0;
        var orderItems = new List<OrderItem>();

        // 2. Her bir kitap için stok kontrolü ve OrderItem oluşturma
        foreach (var item in requestDto.OrderItems)
        {
            var book = await _bookRepository.GetByIdAsync(item.BookId);

            if (book == null)
            {
                return ServiceResult<CreateOrderResponseDto>.Fail(
                    $"Book not found. ID: {item.BookId}",
                    HttpStatusCode.NotFound);
            }

            // Stok kontrolü
            if (book.StockQuantity < item.Quantity)
            {
                return ServiceResult<CreateOrderResponseDto>.Fail(
                    $"Insufficient stock. Book: {book.Title}, Current stock: {book.StockQuantity}, Requested: {item.Quantity}",
                    HttpStatusCode.BadRequest);
            }

            // OrderItem oluştur
            var orderItem = new OrderItem
            {
                BookId = book.Id,
                Quantity = item.Quantity,
                UnitPrice = book.Price
            };

            orderItems.Add(orderItem);
            totalPrice += book.Price * item.Quantity;

            // Stok miktarını güncelle
            book.StockQuantity -= item.Quantity;
            _bookRepository.Update(book);
        }

        // 3. Order oluştur
        var order = new Order
        {
            OrderDate = DateTime.UtcNow,
            TotalPrice = totalPrice,
            Status = Order.OrderStatus.PENDING,
            OrderItems = orderItems
        };


        await _orderRepository.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<CreateOrderResponseDto>.Success(new CreateOrderResponseDto
        {
            Id = order.Id,
            TotalPrice = order.TotalPrice,
            OrderDate = order.OrderDate
        });
    }

    public async Task<ServiceResult> UpdateStatusAsync(int id, UpdateOrderStatusRequestDto requestDto)
    {
        var order = await _orderRepository.GetByIdAsync(id);

        if (order is null)
        {
            return ServiceResult.Fail("Order not found.", HttpStatusCode.NotFound);
        }

        // Status string'ini enum'a çevirme
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