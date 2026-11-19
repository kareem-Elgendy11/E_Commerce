using Application.Dtos.CartDto;
using Application.Interfaces.Reposatoriese;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepo repo;
        private readonly IMapper mapper;
        private readonly IProductRepo productRepo;

        public CartService(ICartRepo repo,IMapper mapper,IProductRepo productRepo)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.productRepo = productRepo;
        }

        public async Task<ApiResponse<string>> AddToCart(string userId, AddToCartDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return ApiResponse<string>.Faild("UserId is null or empty");
                }
                var cart = await repo.GetCart(userId);
                if (cart == null) {
                return ApiResponse<string>.Faild("An Error in User");
                }
                var product = await productRepo.GetProductById(dto.ProductId);
                var productcaritem = await repo.GetCartItemByProductId(dto.ProductId,userId);
                if (productcaritem == null)
                {
                    var cartitem = new CartItem()
                    {
                        Image = "",
                        Name =product.Name,
                        TotalPrice = product.Price * dto.Quantity,
                        Quantity = dto.Quantity,
                        ProductId = product.Id,
                        CartId = cart!.Id,
                    };
                    await repo.AddToCart(cartitem);
                    await repo.save();
                    return ApiResponse<string>.Success("Product add To Cart Successfully");

                }
                productcaritem.Quantity += dto.Quantity;
                productcaritem.TotalPrice += dto.Quantity * product.Price;
                await repo.Update(productcaritem);
                await repo.save();
                return ApiResponse<string>.Success("Product add To Cart Successfully");

            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Faild(ex.Message);
            }
            
        }

        public async Task<ApiResponse<string>> DeleteAll(string UserId)
        {
            try
            {
                await repo.deleteAll(UserId);
                await repo.save();
                return ApiResponse<string>.Success("Cart Is Empty Now");
            }catch(Exception ex)
            {
               return ApiResponse<string>.Faild(ex.Message);
            }
        }

        public async Task<ApiResponse<string>> DeleteProductFromCart(string CartItemId,string UserId)
        {
            try
            {
                if(string.IsNullOrEmpty(CartItemId))
                {
                    return ApiResponse<string>.Faild("Cart In Valid");
                }
                var cart = await repo.GetCart(UserId);
                if (cart != null)
                {
                    var caritem = await repo.GetCartItemById(CartItemId,cart.Id);
                    if (caritem == null)
                    {
                        return ApiResponse<string>.Faild("Item Not Found In Cart");
                    }
                    await repo.Delete(caritem,UserId);
                    await repo.save();
                    return ApiResponse<string>.Success("Item Is Deleted Now");
                }
                return ApiResponse<string>.Faild("Error");
            }
            catch(Exception ex)
            {
                return ApiResponse<string>.Faild(ex.Message);
            }
        }

        public async Task<ApiResponse<List<ProductCart>>> GetAll(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return ApiResponse<List<ProductCart>>.Faild("UserId is null or empty");

            }
            else
            {
                var result = await repo.GetAll(UserId);
                var mappedResult = mapper.Map<List<ProductCart>>(result);
                return ApiResponse<List<ProductCart>>.Success(mappedResult);
            }
        }

        public async Task<ApiResponse<string>> UpdateQuantity(string UserId, UpdateQuantity Dto)
        {
            try
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    return ApiResponse<string>.Faild("UserId is null or empty");
                }
                else
                {
                    CartItem item = await repo.GetCartItemByProductId(Dto.ProductId,UserId);
                    if (item == null)
                    {
                        return ApiResponse<string>.Faild("Product Not Found InCart");
                    }
                        item.TotalPrice = Dto.Quantity * (item.TotalPrice / item.Quantity);
                        item.Quantity = Dto.Quantity;
                    //var mappercart = mapper.Map<CartItem>(Dto);
                     await repo.Update(item);
                     await repo.save();
                        return ApiResponse<string>.Success("Quantity updated successfully");

                }
            }catch(Exception ex)
            {
                return ApiResponse<string>.Faild($"An error occurred: {ex.Message}");
            }
        }
    }
}
