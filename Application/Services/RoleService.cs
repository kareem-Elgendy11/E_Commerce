using Api;
using Application.Dtos.Roles;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleService(IMapper mapper,RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            this.mapper = mapper;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task<ApiResponse<string>> Assign(AssignRoleDto dto)
        {
            if (dto != null)
            {
                if(dto.UserId!=null)
                {
                    var user=await userManager.FindByIdAsync(dto.UserId);
                    if(user!=null)
                    {
                        var assignrole=await userManager.AddToRoleAsync(user,dto.RoleName);
                        if(assignrole.Succeeded)
                        {
                            return  ApiResponse<string>.Success($"{user.UserName} Become {dto.RoleName} Success");
                        }
                        return ApiResponse<string>.Faild(string.Join("\n",assignrole.Errors.Select(e=>$"{e.Description} \n")));
                    }
                    return ApiResponse<string>.Faild("User Not Found");
                }
                return ApiResponse<string>.Faild("UserId Is Required");
            }
            return ApiResponse<string>.Faild("field Is Required");
        }

        public async Task<ApiResponse<string>> CreateRole(CreateRoleDto dto)
        {
            if(dto != null)
            {
                var roleexist = await roleManager.RoleExistsAsync(dto.Name);
                if(roleexist)
                {
                    return ApiResponse<string>.Faild("Role IS Already Exist");
                }
                //var createrole=mapper.Map<IdentityRole>(dto);
               var rolecreate= await roleManager.CreateAsync(new IdentityRole(dto.Name));
                if(!rolecreate.Succeeded)
                {
                   return ApiResponse<string>.Faild(string.Join("\n",rolecreate.Errors.Select(e=>e.Description)));
                }
                return ApiResponse<string>.Success("Role Is Created Successfully");
            }
           return ApiResponse<string>.Faild("Name Is Required");
        }

        public async Task<ApiResponse<List<RoleResDto>>> GetAllRoles()
        {
            var roles= roleManager.Roles.ToList();
           var maproles= mapper.Map<List<RoleResDto>>(roles);
            return ApiResponse<List<RoleResDto>>.Success(maproles);
        }
    }
}
