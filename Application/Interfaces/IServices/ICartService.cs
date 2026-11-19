using Application.Dtos.CartDto;
using Application.Dtos.CategoryDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IServices
{
    public interface ICartService
    {
        public Task<ApiResponse<string>> AddToCart(string userId,AddToCartDto dto);
        public Task<ApiResponse<List<ProductCart>>> GetAll(string UserId);
        public Task<ApiResponse<string>> UpdateQuantity(string UserId, UpdateQuantity Dto);
        public Task<ApiResponse<string>> DeleteProductFromCart(string CategoryId,string UserId);
        public Task<ApiResponse<string>> DeleteAll(string UserId);
        //Task GetAll();
    }
}
