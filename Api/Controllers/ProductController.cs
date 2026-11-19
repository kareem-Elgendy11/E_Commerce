using Application.Dtos.ProductDto;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductservice productservice;

        public ProductController(IProductservice productservice)
        {
            this.productservice = productservice;
        }
        [HttpGet("All")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await productservice.GetAll());
        }
        [HttpPost("Create")]
        public async Task<ActionResult> CreateAsync(CreateProductDto dto)
        {
            if (ModelState.IsValid)
            {
                var result=await productservice.CreateProduct(dto);
                if(result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);

            }
            return BadRequest(ModelState);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult> GetProductById(string Id)
        {
            if (ModelState.IsValid)
            {
                var result = await productservice.GetProductById(Id);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteAsync(string Id)
        {
            var result =await productservice.DeleteProduct(Id);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result);
        }
        [HttpPut("{ProductId}")]
        public async Task<ActionResult> UpdateProduct(string ProductId,UpdateProductDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await productservice.UpdateProduct(ProductId, dto);
                if (result.IsSuccess)
                {
                    return NoContent();
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }

    }
}
