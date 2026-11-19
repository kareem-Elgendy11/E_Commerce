using Application.Dtos.CategoryDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Reposatoriese
{
    public interface ICategoryRepo
    {
        public Task<List<Category>> GetAll();
        public Task<Category> GetCategoryById(string categoryId);
        public Task CreateCategory(Category category);
        public Task UpdateCateory(Category category);
        public Task DeleteCategory(Category category);
        public Task Save();
    }
}
