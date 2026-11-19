using Application.Dtos.ProductDto;
using Application.Interfaces.Reposatoriese;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ProductServics : IProductservice
    {
        private readonly IProductRepo repo;
        private readonly IMapper mapper;
        private readonly ICategoryRepo crepo;

        public ProductServics(IProductRepo repo,IMapper mapper,ICategoryRepo crepo)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.crepo = crepo;
        }
        public async Task<ApiResponse<string>> CreateProduct(CreateProductDto dto)
        {
           if(dto!=null)
            {
                try
                {
                    var product = mapper.Map<Product>(dto);
                    await repo.CreateProduct(product);
                    await repo.Save();
                    return ApiResponse<string>.Success("Product Created Successfully");

                }
                catch (Exception ex)
                {
                    return ApiResponse<string>.Faild(ex.Message);
                }
            }
            return ApiResponse<string>.Faild("Should Enter Data of product");

        }

        public async Task<ApiResponse<string>> DeleteProduct(string productId)
        {
            Product? product =await repo.GetProductById(productId);
            if(product!=null)
            {
                try
                {
                    await repo.DeleteProduct(product);
                    await repo.Save();
                    return ApiResponse<string>.Success("Product Deleted Successfully");
                }catch (Exception ex)
                {
                    return ApiResponse<string>.Faild(ex.Message);

                }
            }
            return ApiResponse<string>.Faild("Product Not found");
        }

        public async Task<ApiResponse<List<ProductDto>>> GetAll()
        {
            var listproduct = await repo.GetAll();
            var listproductmap=mapper.Map<List<ProductDto>>(listproduct);
                    return ApiResponse<List<ProductDto>>.Success(listproductmap);
            
        }

        public async Task<ApiResponse<ProductDto>> GetProductById(string productId)
        {
            try
            {
                var product = await repo.GetProductById(productId);
                if (product != null)
                {
                    var mapproduuct = mapper.Map<ProductDto>(product);
                    return ApiResponse<ProductDto>.Success(mapproduuct);
                }
                return ApiResponse<ProductDto>.Faild("Product Not Found");
            }
            catch(Exception ex)
            {
                return  ApiResponse<ProductDto>.Faild(ex.Message);

            }

        }

        public async Task<ApiResponse<string>> UpdateProduct(string ProductId,UpdateProductDto dto)
        {
            try
            {
                if(string.IsNullOrEmpty(ProductId)||dto==null||dto.Id!=ProductId)
                {
                    return ApiResponse<string>.Faild("Product Not Found");
                }
                var product= await repo.GetProductById(ProductId);
                if (product != null)
                {
                    if(await  crepo.GetCategoryById(dto.CategoryId)==null)
                    { return ApiResponse<string>.Faild("Category Not Found"); }
                    var productmap = mapper.Map(dto,product);
                    //await repo.UpdateProduct(productmap);
                    await repo.Save();
                    return ApiResponse<string>.Success("Update Is Succefully");
                }
                return ApiResponse<string>.Faild("Product Not Found");

            }
            catch(Exception ex)
            {
                return ApiResponse<string>.Faild(ex.Message);
            }
        }
    }
}
