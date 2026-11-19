using Application.Dtos.User;
using Microsoft.AspNetCore.Http;
namespace Application.Interfaces.IServices
{
    public interface IUser
    {
        public Task<ApiResponse<UserProfileDto>> Profile(string id);
        public Task<ApiResponse<string>> UpdateAccount(string Id ,UpdateuserDto dto);
        public Task<bool> DeleteAccount(string id);
        public Task<ApiResponse<UserResDto>> GetUserById(string id);
        public Task<ApiResponse<List<UserResForadminDto>>> GetAllUser();
        public Task<ApiResponse<string>> updateImage(IFormFile file, string UserId);
        public Task<ApiResponse<string>> lockAccount(string Id);
        public Task<ApiResponse<string>> UnlockAccount(string Id);
        public Task<ApiResponse<object>> AccountStatus(string Id);
    }
}
