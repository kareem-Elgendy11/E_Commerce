using Application.Dtos.CategoryDto;
using Application.Interfaces.Reposatoriese;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Reposatories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly E_COmmerceDbContext db;

        public CategoryRepo(E_COmmerceDbContext db)
        {
            this.db = db;
        }
        public async Task CreateCategory(Category category)
        {
            await db.Categories.AddAsync(category);
        }

        public async Task<Category> GetCategoryById(string categoryId)
        {
            return await db.Categories.Where(c => c.Id == categoryId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task DeleteCategory(Category category)
        {
             db.Categories.RemoveRange(category);
               
            
        }

        public async Task<List<Category>> GetAll()
        {
            return await db.Categories.ToListAsync();
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

        public async Task UpdateCateory(Category category)
        {
            db.Categories.UpdateRange(category);
           
        }

    }
}
