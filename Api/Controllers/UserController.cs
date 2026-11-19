using Application.Dtos.User;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser user;
        private string GetUserId() =>  User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        public UserController(IUser user)
        {
            this.user = user;
        }
        [HttpGet("All")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> GetAllUserAsync()
        {
           var result= await user.GetAllUser();
            return Ok(result);
        }
        [HttpGet("Me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            var result=await user.Profile(GetUserId());
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
            
        }
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            
            var result = await user.DeleteAccount(GetUserId());
            if (result)
            {
                return Ok(new { value="Account Is Deleted" });
            }
            return NotFound((new { value = "Account Not found" }));

        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUserById(string Id)
        {
            var result = await user.GetUserById(Id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
        [HttpPost("Upload-Image/id")]
        [Authorize]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if(file.Length==0||file==null)
            { return NotFound("enter file Please"); }
            var id = GetUserId();
            var result = await user.updateImage(file,id);//GetUserId());
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("lockAccount/{Id}")]
        [Authorize]
        public async Task<IActionResult> LockAccount(string Id)
        {
            var result = await user.lockAccount(Id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("UnlockAccount/{Id}")]
        //[Authorize]
        public async Task<IActionResult> UnlockAccount(string Id)
        {
            var result = await user.UnlockAccount(Id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("{Id}/status")]
        public async Task<IActionResult> AccountStatus(string Id)
        {
            var result = await user.AccountStatus(Id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UpdateuserDto dto)
        {
            var result = await user.UpdateAccount(GetUserId(),dto);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result);
        }

    }
}
