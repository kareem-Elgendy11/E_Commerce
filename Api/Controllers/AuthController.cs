using Application.Common;
using Application.Dtos.AccountDto;
using Application.Dtos.AuthDto;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth auth;
        //private readonly HttpContext httpContext;
        //private string getuserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        //private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public AuthController(IAuth auth)//, HttpContext httpContext)
        {
            this.auth = auth;
            //this.httpContext = httpContext;
        }
        [HttpPost("login")]
        [EnableRateLimiting("retelimit")]
        public async Task<ActionResult> Login(loginDto dto)
        {
            if (ModelState.IsValid)
            {
                var resultlogin=await  auth.Login(dto);
                if(resultlogin.IsSuccess)
                {
                    setrefreshtokenincookie(resultlogin.Value!.RefreshToken!, resultlogin.Value.ExpireRefreshAt);
                    return Ok(new {issuccess=resultlogin.IsSuccess,resultlogin.Value.Accesstoken, resultlogin.Value.ExpireAt, resultlogin.Value.user });
                }
                return BadRequest(new { resultlogin.IsSuccess,resultlogin.Error });
            }
            return BadRequest(ModelState);
        }
        [Authorize]
        [HttpGet("logout")]
        public async Task<ActionResult> LogoutAsync()
        {
            var token = HttpContext.Request.Cookies["refreshToken"]!;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApiResponse<string> result = await auth.RevokedToken(token, userId!);
            if (result.IsSuccess)
            {
                HttpContext.Response.Cookies.Delete("RefreshToken");
                return Ok("Logout");
            }
            return BadRequest(result);

        }
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await auth.Register(dto);
                if (result.IsSuccess == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("Resend-Confirm-Email")]
        [EnableRateLimiting("retelimit")]
        public async Task<ActionResult> ResendEmail(string email)
        {
            if (ModelState.IsValid)
            {
                var result = await auth.ResendConfirmEmail(email);
                if (result.IsSuccess == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("ConfirmEmail")]
        [EnableRateLimiting("retelimit")]
        public async Task<ActionResult> ConfirmEmail(string token, string email)
        {
            if (ModelState.IsValid)
            {
                var result = await auth.ConfirmEmail(token, email);
                if (result.IsSuccess == false)
                {
                    return BadRequest(result);
                }
                //setrefreshtokenincookie(result.Value!.RefreshToken!);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("sendResetPassword")]
        [EnableRateLimiting("retelimit")]
        public async Task<ActionResult> SendResetPassword(string email)
        {
            if (ModelState.IsValid)
            {
                var result = await auth.sendResetPassword(email);
                if (result.IsSuccess == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("ConfirmResetPassword")]
        [EnableRateLimiting("retelimit")]
        public async Task<ActionResult> ConfirmResetPassword(string token, string email, ResetPasswordDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await auth.ConfirmResetPassword(token, email, dto.NewPassword);
                if (result.IsSuccess == false)
                {
                    return BadRequest(result);

                }
                return Ok(result);

            }
            return BadRequest(ModelState);
        }
        [HttpPost("ChangePassword")]
        //[Authorize]
        public async Task<ActionResult> ChangePassword(ChangePassowrdDto dto,string id)
        {
            if (ModelState.IsValid)
            {
                var result = await auth.ChangePassword(dto, id);//getuserId());
                if (result.IsSuccess == false)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("RefreshToken/{userId}")]
        //[AllowAnonymous]
        public async Task<ActionResult> RefreshToken(string userId)
        {
            //var princaple=getp
            var token = HttpContext.Request.Cookies["refreshToken"]!;
            //var userId = princ.FindFirstValue(ClaimTypes.NameIdentifier);
            //var userId = getuserId();
            var result = await auth.RefreshToken(token,userId);
            if (result.IsSuccess == false)
            {
                return BadRequest(result);
            }
            setrefreshtokenincookie(result.Value!.RefreshToken!, result.Value.ExpireRefreshAt);
            return Ok(new { issuccess = result.IsSuccess, result.Value!.Accesstoken, result.Value.ExpireAt, result.Value.user });

        }
        private void setrefreshtokenincookie(string token,DateTime expire)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(3),
                SameSite = SameSiteMode.Lax,
                Secure = false,
                Path="/"
            };
            HttpContext.Response.Cookies.Append("RefreshToken", token, cookieOptions);
        }
    }
}
