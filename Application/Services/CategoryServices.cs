using Application.Dtos.CategoryDto;
using Application.Interfaces.Reposatoriese;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace Application.Services
{
    public class CategoryServices : ICategoryService
    {
        private readonly IMapper mapper;
        private readonly ICategoryRepo repo;

        public CategoryServices(IMapper mapper,ICategoryRepo repo)
        {
            this.mapper = mapper;
            this.repo = repo;
        }
        public async Task<ApiResponse<string>> CreateCategory(CreateCategoryDTO dto)
        {
            try
            {
                var mapcategory = mapper.Map<Category>(dto);
                await repo.CreateCategory(mapcategory);
                await repo.Save();
                return ApiResponse<string>.Success("Category Created Successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Faild(ex.Message);
            }

        }

        public async Task<ApiResponse<string>> DeleteCategory(string CategoryId)
        {
            try
            {
                if (string.IsNullOrEmpty(CategoryId))
                {
                    return ApiResponse<string>.Faild("Id IS In Valid");
                }
                 var category=await repo.GetCategoryById(CategoryId);
                if(category == null)
                {
                    return ApiResponse<string>.Faild("Category Not Found");
                }
                await repo.DeleteCategory(category);
                await repo.Save();
                return ApiResponse<string>.Success("Deleted done ");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Faild(ex.Message);
            }
        }

        public async Task<ApiResponse<List<AllCategoryDTO>>> GetAll(int page=1)
        {
            int pagesize = 10;
            var categoryist = await repo.GetAll();
           var x= categoryist.Skip(page * pagesize).Take(pagesize).ToList();
            var totalpage = (Math.Ceiling((double)categoryist.Count()/pagesize));
            var categoryMapp = mapper.Map<List<AllCategoryDTO>>(categoryist);
            return ApiResponse<List<AllCategoryDTO>>.Success(categoryMapp);
        }

        public async Task<ApiResponse<string>> UpdateCategory(string CategoryId, AllCategoryDTO dto)
        {
            try
            {
                if (string.IsNullOrEmpty(CategoryId) || dto == null||CategoryId!=dto.Id)
                {
                    return ApiResponse<string>.Faild("Id IS In Valid");
                }
                var category=repo.GetCategoryById(CategoryId);
                if(category == null)
                {
                    return ApiResponse<string>.Faild("Category Not Found");
                }
                var mappercategoryupdate=mapper.Map<Category>(dto);
                await repo.UpdateCateory(mappercategoryupdate);
                await repo.Save();
                return ApiResponse<string>.Success("Update Is Successs");

            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Faild(ex.Message);
            }


        }
    }
}
