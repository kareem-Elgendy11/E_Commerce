using Application.Dtos.ProductImageDto;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService service;

        public ProductImagesController(IProductImageService service)
        {
            this.service = service;
        }
        [HttpGet("All/{productId}")]
        public async Task<ActionResult> GetAll(string productId)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await service.GetAll(productId);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost]
        public async Task<ActionResult> AddImages([FromForm]AddImagesDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await service.AddImages(dto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateImages([FromForm]UpdateImagedto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await service.UpdateProductImage(dto);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteImages(string ImageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await service.DeleteProductImage(ImageId);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result);
        }
    }
}
