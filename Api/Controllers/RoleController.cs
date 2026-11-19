using Application.Dtos.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }
        [HttpPost("CreateRole")]
        public async Task<ActionResult> CreateRole(CreateRoleDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await roleService.CreateRole(dto);
                if (result.IsSuccess)
                {
                    return Created("",result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("Change-role")]
        public async Task<ActionResult> Changerole(AssignRoleDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await roleService.Assign(dto);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result);
        }
        [HttpGet("All")]
        public async Task<ActionResult> GetAll()
        {
            var result= await roleService.GetAllRoles();
                    return Ok(result);

        }
    }
}
