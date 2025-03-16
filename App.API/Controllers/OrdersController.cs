using App.Services.Books;
using App.Services.Orders;
using App.Services.Orders.Create;
using App.Services.Orders.UpdateStatus;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var orderResult = await _orderService.GetByIdAsync(id);
        return new ObjectResult(orderResult)
        {
            StatusCode = orderResult.HttpStatusCode.GetHashCode()
        };
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequestDto requestDto)
    {
        var createResult = await _orderService.CreateAsync(requestDto);
        return new ObjectResult(createResult)
        {
            StatusCode = createResult.HttpStatusCode.GetHashCode()
        };
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderStatusRequestDto requestDto)
    {
        var result = await _orderService.UpdateStatusAsync(id, requestDto);
        return new ObjectResult(result)
        {
            StatusCode = result.HttpStatusCode.GetHashCode()
        };
    }
}

