using Application.Dtos.Roles;

namespace Api
{
    public interface IRoleService
    {
        public Task<ApiResponse<string>> CreateRole(CreateRoleDto dto);
        public Task<ApiResponse<string>> Assign(AssignRoleDto dto);
        public Task<ApiResponse<List<RoleResDto>>> GetAllRoles();


    }
}
