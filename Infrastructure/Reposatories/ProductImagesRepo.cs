using Application.Interfaces.Reposatoriese;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Reposatories
{
    public class ProductImagesRepo:IProductImageRepo
    {
        private readonly E_COmmerceDbContext db;

        public ProductImagesRepo(E_COmmerceDbContext db)
        {
            this.db = db;
        }
        public async Task <List<ProductImages>> GetAll(string ProductId)
        {
           return await db.ProductImages.Where(i => i.ProductId == ProductId).ToListAsync();
        }

        public async Task<bool> DeleteImage(ProductImages images)
        {
            db.ProductImages.Remove(images);
            return true;

        }

        public async Task AddImage(ProductImages images)
        {
             await db.ProductImages.AddAsync(images);
        }

        public async Task<ProductImages> GetImageById(string imageId)
        {
            var image = await db.ProductImages.Where(i => i.Id == imageId).FirstOrDefaultAsync();
            return image!;
        }

        public async Task<bool> UpdateImage(ProductImages images)
        {
            var update = db.ProductImages.Update(images);
            return true;
        }
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
