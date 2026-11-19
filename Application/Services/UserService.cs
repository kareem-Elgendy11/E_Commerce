using Application.Dtos.User;
using Application.Interfaces.Reposatoriese;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class UserService : IUser
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IUserRepo repo;
        private readonly IFileServices fileservice;
        private readonly IAuth auth;

        public UserService(UserManager<ApplicationUser> userManager,IMapper mapper,IUserRepo repo,IFileServices fileservice,IAuth auth)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.repo = repo;
            this.fileservice = fileservice;
            this.auth = auth;
        }
        public async Task<ApiResponse<object>> AccountStatus(string Id)
        {
            //throw new NotImplementedException();
            var user = await userManager.FindByIdAsync(Id);
            if(user!=null)
            {
                var isLockOut=await userManager.IsLockedOutAsync(user);
                return  ApiResponse<object>.Success(new { user.Id,user.Email,user.EmailConfirmed, user.CreatedAt, user.LockoutEnd,isLockOut,  });
            }
            return ApiResponse<object>.Faild("UserID Is Not Exist");

        }

        public async Task<bool> DeleteAccount(string id)
        {
            //throw new NotImplementedException();
            var isDeleted=await repo.DeleteAccount(id);
            if(isDeleted)
            {
                await repo.Save();
                return true;
            }
            return false;

        }

        public async Task<ApiResponse<List<UserResForadminDto>>> GetAllUser()
        {
            //throw new NotImplementedException();
            var users = await repo.GetAll();
            var userDtoList=  mapper.Map<List<UserResForadminDto>>(users);
            return ApiResponse<List<UserResForadminDto>>.Success(userDtoList);
        }

        public async Task<ApiResponse<UserResDto>> GetUserById(string id)
        {
            var user=await repo.GetUserById(id);
            if(user==null)
            {
                return ApiResponse<UserResDto>.Faild("User Not found");
            }
            var mapuser = mapper.Map<UserResDto>(user);
            return ApiResponse<UserResDto>.Success(mapuser);
        }

        public async Task<ApiResponse<string>> lockAccount(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if(user!=null)
            {
               if( await userManager.IsLockedOutAsync(user))
                { return ApiResponse<string>.Faild("Account Is Locked Already"); }
               await userManager.SetLockoutEnabledAsync(user, true);
                await userManager.SetLockoutEndDateAsync(user,DateTime.UtcNow.AddDays(10));
                return ApiResponse<string>.Success("Account Is Locked Successfully");
            }
            return ApiResponse<string>.Faild("User Not Found"); 
        }

        public async Task<ApiResponse<UserProfileDto>> Profile(string id)
        {
            var user=await repo.GetUserById(id);
            if(user==null )
            {
                return ApiResponse<UserProfileDto>.Faild("User Not Found");
            }
            return ApiResponse<UserProfileDto>.Success(mapper.Map<UserProfileDto>(user));
        }

        public async Task<ApiResponse<string>> UnlockAccount(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if(user!=null)
            {
                if( await userManager.IsLockedOutAsync(user))
                {
                    await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
                    return ApiResponse<string>.Success("Account Is Unlocked Successfully");
                }
                return ApiResponse<string>.Faild("Account Is Not Locked");
            }
            return ApiResponse<string>.Faild("User Not Found");
        }

        public async Task<ApiResponse<string>> UpdateAccount(string Id,UpdateuserDto dto)
        {
            var user=await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                if (dto.UserName != null && user!.UserName != dto.UserName)
                {
                    var setusername=await userManager.SetUserNameAsync(user, dto.UserName);
                    if(!setusername.Succeeded)
                    {
                        ApiResponse<string>.Faild(string.Join("\n", setusername.Errors.Select(e => e.Description)));
                    }
                }
                if (dto.FIrstName != null && user!.FirstName != dto.FIrstName)
                {
                    user.FirstName = dto.FIrstName;
                }
                if (dto.LastName != null && user!.LastName != dto.LastName)
                {
                    user.LastName = dto.LastName;
                }
                if (dto.Address != null && user!.Address != dto.Address)
                {
                    user.Address = dto.Address;
                }
                if (dto.Phone != null && user!.PhoneNumber != dto.Phone)
                {
                    await userManager.SetPhoneNumberAsync(user, dto.Phone);
                }
                if (dto.Email != null && user!.Email != dto.Email)
                {
                    var setEmail = await userManager.SetEmailAsync(user, dto.Email);
                    if (!setEmail.Succeeded)
                    {
                        return ApiResponse<string>.Faild(string.Join("\n", setEmail.Errors.Select(e => e.Description)));
                    }
                    await auth.ResendConfirmEmail(dto.Email);

                }
                await userManager.UpdateAsync(user);
                return ApiResponse<string>.Success("Data Is Updated");
            }
            return ApiResponse<string>.Faild("Enter Data Or User Not Found");

        }

        public async Task<ApiResponse<string>> updateImage(IFormFile file, string UserId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(UserId);
                if (user != null)
                {
                    if(user.ImagrUrlProfile!=null)
                    {
                        if (File.Exists(user.ImagrUrlProfile)) File.Delete(user.ImagrUrlProfile);
                    }
                    var url = await fileservice.SaveImage(file, "UserImages");
                    if (url != null)
                    {
                        user.ImagrUrlProfile = url;
                        await userManager.UpdateAsync(user);
                      return  ApiResponse<string>.Success(url);
                    }
                  return  ApiResponse<string>.Faild("The File IS Large ");
                }
                return ApiResponse<string>.Faild("User Not Found");
            }catch(Exception e)
            {
                return ApiResponse<string>.Faild(e.Message);
            }
        }
    }
}
