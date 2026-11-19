using Application.Dtos.CategoryDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IServices
{
    public interface ICategoryService
    {
        public Task<ApiResponse<List<AllCategoryDTO>>> GetAll(int page);
        public Task<ApiResponse<string>> CreateCategory(CreateCategoryDTO dto);
        public Task<ApiResponse<string>> UpdateCategory(string CategoryId, AllCategoryDTO dto);
        public Task<ApiResponse<string>> DeleteCategory(string CategoryId);

    }
}
