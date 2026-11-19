using Application.Dtos.OrderDto;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService service;

        public OrderController(IOrderService service)
        {
            this.service = service;
        }
        private string getbyid() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        [HttpGet("My")]
        public async Task<IActionResult> GetMyOrder()
        {
            var myorder=await service.GetMyOrdersAsync(getbyid());
            if (myorder.IsSuccess)
            {
                return Ok(myorder);
            }
            return BadRequest(myorder);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            var result = await service.CreateOrderAsync(getbyid());
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("Cancel/{orderId}")]
        public async Task<IActionResult> CancelOrder(string orderId)
        {
            var result = await service.CancelOrderAsync(orderId,getbyid());
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("MarkAsDelivered/{orderId}")]
        public async Task<IActionResult> MarkAsDelivered(string orderId)
        {
            var result = await service.MarkAsDelivered(orderId, getbyid());
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
