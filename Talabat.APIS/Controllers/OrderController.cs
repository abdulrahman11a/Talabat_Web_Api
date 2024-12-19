using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIS.DTOs.Order;
using Talabat.APIS.Errors;
using Talabat.core.Entitys.Order_Aggregate;
using Talabat.core.Entitys.Order_Aggregate.Enum_Order_Aggregate;
using Talabat.core.Services.Contract;

namespace Talabat.APIS.Controllers
{
    
    [Authorize]
    
    public class OrderController(IOrderService _orderService, IMapper _mapper) : ApiBaseController
    {

        [HttpPost]
        public async Task<ActionResult<ResponseOrderDto>> CreateOrder([FromBody] RequestOrderDto orderDto)
        {
            if (orderDto == null)
                return BadRequest(new ApiResponse(400, "Invalid order data."));

            var address = _mapper.Map<Address>(orderDto.Address);

            try
            {
                var createdOrder = await _orderService.CreateOrderAsync(
                    buyerEmail: User.FindFirstValue(ClaimTypes.Email) ,
                    basketId: orderDto.BasketId,
                    deliveryMethodId: orderDto.DeliveryMethodId,
                    address: address,   
                    discountType: Discount.NoDiscount,
                    isMember: orderDto.IsMember
                );

                if (createdOrder == null)
                    return BadRequest(new ApiResponse(400, "Order could not be created."));

                var response = _mapper.Map<ResponseOrderDto>(createdOrder);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, $"Server error: {ex.Message}"));
            }
        }

        [HttpGet("GetOrdersForUserByEmail")]
        public async Task<ActionResult<IReadOnlyList<ResponseOrderDto>>> GetOrdersForUserByEmail()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(Email))
                return BadRequest(new ApiResponse(401));

            var orders = await _orderService.GetOrdersForUserAsync(Email);

            var response = _mapper.Map<IReadOnlyList<ResponseOrderDto>>(orders);

            return orders == null || !orders.Any()
                ? NotFound(new ApiResponse(404, "No orders found for this email."))
                : Ok(response);
        }

        [HttpGet("GetSpecificOrderForUser")]
        public async Task<ActionResult<ResponseOrderDto>> GetSpecificOrderForUser(int orderId)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (orderId <= 0 || string.IsNullOrEmpty(Email))
                return BadRequest(new ApiResponse(400, "Invalid order ID or not authorized."));

            var order = await _orderService.GetOrderByIdForUserAsync(orderId, Email);
            var response = _mapper.Map<ResponseOrderDto>(order);

            return order == null
                ? NotFound(new ApiResponse(404, "Order not found for the given ID and email."))
                : Ok(response);
        }


        [HttpGet("GetDelivery")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDelivery()
        {
     
                var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();

                if (deliveryMethods == null || !deliveryMethods.Any())
                    return NotFound(new ApiResponse(404, "No delivery methods found."));

                return Ok(deliveryMethods);

        }
        //[HttpGet("Get curent user")]


    }
}
