using Application.Dtos.ProductDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Reposatoriese
{
    public interface IProductRepo
    {
        public Task<Product> GetProductById(string Id);
        public Task<List<Product>> GetAll();
        public Task CreateProduct(Product product);
        public Task<bool> UpdateProduct(Product product);
        public Task<bool> DeleteProduct(Product product);
        public  Task Save();
    }
}
