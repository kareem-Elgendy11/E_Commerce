using Application.Dtos.CartDto;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private string GetUserID() => User.FindFirstValue(ClaimTypes.NameIdentifier)!.ToString();
        private readonly ICartService service;

        public CartController(ICartService service)
        {
            this.service = service;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> Getall()
        {
            var result = await service.GetAll(GetUserID());
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("Delete/{Id}")]
        public async Task<ActionResult> Delete(string Id)
        {
            var result = await service.DeleteProductFromCart(Id, GetUserID());
            if(result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result);
        }
        [HttpDelete("DeleteALl")]
        public async Task<ActionResult> DeleteAll()
        {
            var result = await service.DeleteAll(GetUserID());
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result);
        }
        [HttpPost("AddToCart")]
        public async Task<ActionResult> AddToCart(AddToCartDto dto)
        {
            var result = await service.AddToCart(GetUserID(),dto);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result);
        }
        [HttpPut("UpdateQuantity")]
        public async Task<ActionResult> EditCart(UpdateQuantity dto)
        {
            var result = await service.UpdateQuantity(GetUserID(), dto);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result);
        }
    }
}
