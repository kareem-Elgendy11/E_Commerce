using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Reposatoriese
{
    public interface IProductImageRepo
    {
        public  Task<List<ProductImages>> GetAll(string ProductId);
        public Task<bool> DeleteImage(ProductImages images);
        public  Task AddImage(ProductImages images);
        public  Task<ProductImages> GetImageById(string imageId);
        public  Task<bool> UpdateImage(ProductImages images);
        public  Task Save();





    }
}
