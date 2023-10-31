using API.Dtos;
using API.Errors;
using API.Extentions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace API.Controllers;
[Authorize]
public class OrdersController: BaseApiController
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<Order>> createOrdersAsync(OrderDto orderDto)
    {
        var email = HttpContext.User?.RetrieveEmailFromPrincipal();
        var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);

        var order = await _orderService.CreateOrderAsync(email, 
                        orderDto.DeliveryMethodId,orderDto.BasketId,address);

        if (order is null) return BadRequest(new ApiResponse(400, "Problem creating order."));

        return Ok(order);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUserAsync()
    {
        var email = HttpContext.User?.RetrieveEmailFromPrincipal();
        var orders = await _orderService.GetOrdersForUserAsync(email);

        if (orders is null) return BadRequest(new ApiResponse(404));

        return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderToReturnDto>> GetOrderForUserByIdAsync(int id)
    {
        var email = HttpContext.User?.RetrieveEmailFromPrincipal();
        var order = await _orderService.GetOrderByIdAsync(id, email);

        if (order is null) return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<OrderToReturnDto>(order));
    }

    [HttpGet("deliveryMethods")]
    public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethodsAsync()
    {
        var dm = await _orderService.GetDeliveryMethodsAsync();

        if (dm is null) return BadRequest(new ApiResponse(404));

        return Ok(dm);
    }
}
