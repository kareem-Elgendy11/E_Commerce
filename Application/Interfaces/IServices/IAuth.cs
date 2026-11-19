using Application.Common;
using Application.Dtos.AccountDto;
using Application.Dtos.AuthDto;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces.IServices
{
    public interface IAuth
    {
        public Task<ApiResponse<AuthResponseDto>> Login(loginDto dto);
        public Task<ApiResponse<string>> Register(RegisterDto dto);
        //public Task Logout();
        public Task<ApiResponse<AuthResponseDto>> RefreshToken(string token, string userId);
        public Task< ApiResponse<string>> RevokedToken(string token,string userId);
        public  Task<ApiResponse<string>> ConfirmResetPassword(string token, string email, [FromBody] string NewPassword);
        public Task<ApiResponse<string>> sendResetPassword(string Email);
        public Task<ApiResponse<string>> ChangePassword(ChangePassowrdDto dto, string userId, CancellationToken cancellation = default);

        public Task<ApiResponse<string>> ConfirmEmail(string token,string email);
        public Task<ApiResponse<string>> ResendConfirmEmail(string Email);





    }
}
