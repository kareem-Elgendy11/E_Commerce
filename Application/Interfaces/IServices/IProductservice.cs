using Application.Dtos.ProductDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IServices
{
    public interface IProductservice
    {
        public Task<ApiResponse<ProductDto>> GetProductById(string productId);
        public Task<ApiResponse<List<ProductDto>>> GetAll();
        public Task<ApiResponse<string>> CreateProduct(CreateProductDto dto);
        public Task<ApiResponse<string>> UpdateProduct(string productId, UpdateProductDto dto);
        public Task<ApiResponse<string>> DeleteProduct(string productId);

    }
}
