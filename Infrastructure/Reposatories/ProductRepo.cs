using Application.Interfaces.Reposatoriese;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Reposatories
{
    public class ProductRepo : IProductRepo
    {
        private readonly E_COmmerceDbContext db;

        public ProductRepo(E_COmmerceDbContext db)
        {
            this.db = db;
        }
        public async Task CreateProduct(Product product)
        {
            await db.Products.AddAsync(product);
        }

        public async Task<bool> DeleteProduct(Product product)
        {
            db.Products.RemoveRange(product);
            return true;

        }

        public async Task<List<Product>> GetAll()
        {
            return await db.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(string productId)
        {
         var product= await db.Products.Where(p=>p.Id==productId).FirstOrDefaultAsync();
            return product!;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
           var update= db.Products.Update(product);
            return true;
        }
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
