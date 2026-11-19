using Application.Dtos.ProductDto;
using Application.Dtos.ProductImageDto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IServices
{
    public interface IProductImageService
    {
        //public Task<ApiResponse<ProductDto>> GetProductById(string productId);
        public Task<ApiResponse<List<ProductImageAll>>> GetAll(string ProductId);
        public Task<ApiResponse<string>> AddImages(AddImagesDto dto);
        public Task<ApiResponse<string>> UpdateProductImage(UpdateImagedto dto);
        public Task<ApiResponse<string>> DeleteProductImage(string ImageId);
    }
}
