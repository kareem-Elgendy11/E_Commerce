using Application.Dtos.AuthDto;
using Application.Interfaces.Reposatoriese;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    internal class Auth : IAuth
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IsendEmail email;
        private readonly ITokenServices tokenServices;
        private readonly IRefreshTokenRepo repo;
        private readonly ICartRepo crepo;

        //private readonly IHttpContextAccessor httpContext;

        public Auth(UserManager<ApplicationUser> userManager,IsendEmail email,ITokenServices tokenServices,IRefreshTokenRepo repo,ICartRepo crepo)
        {
            this.userManager = userManager;
            this.email = email;
            this.tokenServices = tokenServices;
            this.repo = repo;
            this.crepo = crepo;
            //this.httpContext = httpContext;
        }
        public async Task<ApiResponse< string>> ChangePassword(ChangePassowrdDto dto,string userId,CancellationToken cancellation=default)
        {
            //throw new NotImplementedException();
            var user=await userManager.FindByIdAsync(userId);
            if(user!=null)
            {
                var add = await userManager.AddPasswordAsync(user, dto.OldPassword);
                var CheckPassword = await userManager.CheckPasswordAsync(user, dto.OldPassword);
                if (CheckPassword)
                {
                        var addpassword = await userManager.ChangePasswordAsync(user,dto.OldPassword ,dto.NewPassword);
                        if (addpassword.Succeeded)
                        {
                            await userManager.UpdateSecurityStampAsync(user);
                            return ApiResponse<string>.Success("Change Password Is Successfully");
                        }
                        var error = "";
                        foreach (var item in addpassword.Errors)
                        {
                            error += item.Description+"\n";
                        }
                        return ApiResponse<string>.Faild(string.Join("\n",addpassword.Errors.Select(e=>e.Description)));
                    
                }
            }
            return   ApiResponse<string>.Faild("Old PAssword Is Wrong");
        }

        public async Task<ApiResponse<string>> ConfirmEmail(string token, string email)
        {
            if(!string.IsNullOrEmpty(token)&& !string.IsNullOrEmpty(email))
            {
                var user = userManager.Users.FirstOrDefault(u => u.Email == email && u.EmailConfirmed == false);
                if (user != null)
                {

                    var tokendecode = WebEncoders.Base64UrlDecode(token);
                    var tokenuser = Encoding.UTF8.GetString(tokendecode);
                   var resultConfirmed= await userManager.ConfirmEmailAsync(user, tokenuser);
                    var refreshtoken = new RefreshToken()
                    {
                        Refreshtoken = tokenServices.GenerateRefreshToken(),
                        CreatedAt = DateTime.Now,
                        ExpireAt = DateTime.Now.AddHours(10),
                        UserId = user.Id
                    };
                    await repo.AddRefreshToken(refreshtoken);
                    await repo.SaveChange();
                    var accesstoken = await tokenServices.GenerateAccessToken(user);
                    if (resultConfirmed.Succeeded)
                    {
                        //var x = DateTime.Now;
                        return ApiResponse<string>.Success("Confirm Email Is Successfully Now Can LoginIn");
                    }
                }
            }
            return ApiResponse<string>.Faild("Error in confirm Email");

        }

        public async Task<ApiResponse<AuthResponseDto>> Login(loginDto dto)
        {
            //throw new NotImplementedException();
            try
            {
                var user = await userManager.FindByNameAsync(dto.UserName);
                if (user != null)
                {
                    if (await userManager.IsEmailConfirmedAsync(user))
                    {

                        var checkpassword = await userManager.CheckPasswordAsync(user, dto.Password);
                        if (checkpassword)
                        {
                            if (await userManager.IsLockedOutAsync(user))
                            {
                                return ApiResponse<AuthResponseDto>.Faild("Account is Locked Please Contact Admin");
                            }
                            await userManager.ResetAccessFailedCountAsync(user);
                            await userManager.UpdateSecurityStampAsync(user);
                            var refreshtoken = new RefreshToken()
                            {
                                Refreshtoken = tokenServices.GenerateRefreshToken(),
                                CreatedAt = DateTime.Now,
                                ExpireAt = DateTime.UtcNow.AddHours(10),
                                UserId = user.Id
                            };
                            await repo.AddRefreshToken(refreshtoken);
                            await repo.SaveChange();
                            var accesstoken = await tokenServices.GenerateAccessToken(user);
                            return ApiResponse<AuthResponseDto>.Success(
                                new AuthResponseDto
                                {
                                    RefreshToken=refreshtoken.Refreshtoken,
                                    ExpireRefreshAt=refreshtoken.ExpireAt,
                                    Accesstoken = accesstoken.token,
                                    ExpireAt = accesstoken.expireAt,
                                    user = { ID = user.Id, Email = user.Email!, UserName = user.UserName }
                                });
                        }
                        await userManager.AccessFailedAsync(user);
                        return ApiResponse<AuthResponseDto>.Faild("UserName OR Password Is Wrong");

                    }
                    return ApiResponse<AuthResponseDto>.Faild("Please Confirm Email First ");
                }
                 return ApiResponse<AuthResponseDto>.Faild("UserName OR Password Is Wrong");

            }
            catch (Exception e)
            {
                return ApiResponse<AuthResponseDto>.Faild(e.Message);
            }
        }

        public async Task<ApiResponse<AuthResponseDto>> RefreshToken(string token,string userId)
        {
            try
            {
                    var user= userManager.Users.FirstOrDefault(u=>u.Id==userId);
                    if (user != null)
                    {

                        var getrefreshtoken = await repo.GetRefreshToken(token,user.Id);
                        if(getrefreshtoken!=null&& getrefreshtoken.Refreshtoken == token)
                        {
                            getrefreshtoken.RevokedOn = DateTime.Now;
                            await repo.UpdateRefreshToken(getrefreshtoken);
                            await repo.SaveChange();
                        }
                        var refreshtoken = new RefreshToken()
                        {
                            Refreshtoken= tokenServices.GenerateRefreshToken(),
                            CreatedAt=DateTime.Now,
                            ExpireAt=DateTime.UtcNow.AddHours(10),
                            UserId=user.Id
                        };
                        await repo.AddRefreshToken(refreshtoken);
                        await repo.SaveChange();
                        var accesstoken=await tokenServices.GenerateAccessToken(user);
                        //setrefreshtokenincookie(refreshtoken.Refreshtoken);
                        return ApiResponse<AuthResponseDto>.Success(new AuthResponseDto
                        {
                            RefreshToken = refreshtoken.Refreshtoken,
                            ExpireRefreshAt = refreshtoken.ExpireAt,
                            Accesstoken = accesstoken.token,
                            ExpireAt = accesstoken.expireAt,
                            user = user.UserName != null ? new Dtos.User.UserResDto { ID = user.Id, Email = user.Email!, UserName = user.UserName } : null!
                        });
                    
                    }
                return  ApiResponse<AuthResponseDto>.Faild("User Not Found");
            }
            catch(Exception e)
            {
                //var x = e.Message;
                return  ApiResponse<AuthResponseDto>.Faild(e.Message);
            }
        }


        public async Task<ApiResponse<string>> Register(RegisterDto dto)
        {
            ApplicationUser user=new ApplicationUser()
            {
                UserName= dto.UserName,
                Email=dto.Email,
                PhoneNumber=dto.Phone,
                Address=dto.Address,
                FirstName=dto.FirstName,
                LastName=dto.LastName,
                CreatedAt=DateTime.Now
            };
            var resultuser = await userManager.CreateAsync(user, dto.Password);
            await userManager.AddToRoleAsync(user, "User");
            await userManager.SetLockoutEnabledAsync(user, true);
            if (!resultuser.Succeeded)
            {
                foreach (var error in resultuser.Errors)
                {
                    return ApiResponse<string>.Faild(error.Description);
                }
            }
            var cart=new Cart()
            {
                UserId = user.Id,
                CreatedAt = DateTime.Now
            };
            await crepo.CreateCart(cart);
            await crepo.save();
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodetoken=WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var Body = $"<h1>Hello {dto.UserName} </h1><p>Please Confirm Email by click on the button</p> <a href='https://localhost:7198/api/Auth/ConfirmEmail?token={encodetoken}&email={dto.Email}' class='btn btn-outline-primary'>Confirm</a>";
            var resultsendemail=await email.sendemail(dto.Email, "Confirm Email", Body);
            if(!resultsendemail)
            {
                return ApiResponse<string>.Faild("Failed to send confirmation email,Please Try Resend Confirm Email");
            }

            return ApiResponse<string>.Success("Account is created Successfully,send Email Confirm IsDone Please Check Message");
        }

        public async Task<ApiResponse<string>> ResendConfirmEmail(string Email)
        {
            if(!string.IsNullOrEmpty(Email))
            {
                var user= userManager.Users.FirstOrDefault(u=>u.Email==Email&&u.EmailConfirmed==false);
                if(user==null)
                {
                    return ApiResponse<string>.Faild("Failed to send confirmation email");
                }
                    var isConfirmed = await userManager.IsEmailConfirmedAsync(user);
                    if (isConfirmed)
                    {
                        return ApiResponse<string>.Faild("Account is Confirm already Please Loginin Now");
                    }
                    await userManager.UpdateSecurityStampAsync(user);
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var Encodetoken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                    var ConfirmLink = $"https://localhost:7198/api/Auth/ConfirmEmail?token={Encodetoken}&email={user.Email}";
                    var body = $"<h1>Hello {user.UserName}</h1> <p>Please Confirm Email by Click Here</p><a href='{ConfirmLink}'class='btn btn-outline-primary'>Confirm</a>";
                    var sendemailresult = await email.sendemail(user.Email!, "Confirm email", body);
                    if (sendemailresult)
                    {
                        return ApiResponse<string>.Success("Resend Email Confirm Is Done Please Check Message ");

                    }
            }
            return  ApiResponse<string>.Faild("Failed to send confirmation email");
        }

        public async Task<ApiResponse<string>> sendResetPassword(string Email)
        {
            var user= userManager.Users.FirstOrDefault(u=>u.Email==Email);
            if(user!=null)
            {
                await userManager.UpdateSecurityStampAsync(user);
                var token =await userManager.GeneratePasswordResetTokenAsync(user);
                var encodetoken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var confirmLink = $"https://localhost:7198/api/Auth/ConfirmResetPassword?token={encodetoken}&email={user.Email}";
                var body = $"<h1>Hello {user.UserName}</h1><p> Please click here for Reset Password then enter New Password </p><a href='{confirmLink}' class='btn btn-outline-primary'>Reset Password</a>";
               var resultsendemail= await email.sendemail(user.Email!, "ResetPassword",body);
                if(resultsendemail)
                {
                    return ApiResponse<string>.Success("Reset Password Email Is Send Successfully Please Check Your Email");
                }
            }
            return ApiResponse<string>.Faild("Please Insert Email Is registered");
        }
        public async Task<ApiResponse<string>> ConfirmResetPassword(string token, string email, [FromBody] string NewPassword)
        {
            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(email))
            {
                var user = userManager.Users.FirstOrDefault(u => u.Email == email );
                if (user != null)
                {

                    var tokendecode = WebEncoders.Base64UrlDecode(token);
                    var tokenuser = Encoding.UTF8.GetString(tokendecode);
                    var resultConfirmed = await userManager.ResetPasswordAsync(user, tokenuser, NewPassword);
                    //var refresh=await repo.GetRefreshToken(user.Id);
                    //var refreshtoken= await RefreshToken(refresh.Refreshtoken,user.Id);
                    var accesstoken =await tokenServices.GenerateAccessToken(user);
                    if (resultConfirmed.Succeeded)
                    {
                        return ApiResponse<string>.Success("Password Reset Successfully Can Log in By New Password");
                    }
                    return ApiResponse<string>.Faild( string.Join("\n",resultConfirmed.Errors.Select(e=>e.Description)));

                }
            }
            return ApiResponse<string>.Faild("Error in reset Password");

        }
        public async Task<ApiResponse<string>> RevokedToken(string token,string userId)
        {
            if(token!=null)
            {
                var gettoken = repo.GetRefreshToken(token,userId);
                if (gettoken != null)
                {
                    gettoken.Result.RevokedOn = DateTime.Now;
                    await repo.UpdateRefreshToken(gettoken.Result);
                    await repo.SaveChange();
                    //httpContext.HttpContext!.Response.Cookies.Delete("RefreshToken");
                    return ApiResponse<string>.Success("Token Is Revoked");
                }
            }
            return ApiResponse<string>.Faild("Token Is Error");
        }

    }
}
