using Application.Dtos.CategoryDto;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController( ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpGet("All/{page}")]
        public async Task<IActionResult> Get(int page)
        {
            var result = await categoryService.GetAll(page);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete( string Id)
        {
            var result=await categoryService.DeleteCategory(Id);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(string Id,AllCategoryDTO dto)
        {
            if (ModelState.IsValid)
            {
                var result = await categoryService.UpdateCategory(Id, dto);
                if (result.IsSuccess)
                {
                    return NoContent();
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDTO dto)
        {
            if (ModelState.IsValid)
            {
                var result = await categoryService.CreateCategory( dto);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }
    }
}
