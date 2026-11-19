using Application.Dtos.ProductImageDto;
using Application.Interfaces.Reposatoriese;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ProductImagesService : IProductImageService
    {
        private readonly IProductImageRepo repo;
        private readonly IMapper mapper;
        private readonly IFileServices file;
        private readonly IProductRepo productRepo;

        public ProductImagesService(IProductImageRepo repo,IMapper mapper,IFileServices file,IProductRepo productRepo)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.file = file;
            this.productRepo = productRepo;
        }
        public async Task<ApiResponse<string>> AddImages(AddImagesDto dto)
        {
            try
            {
                if (dto == null || dto.file == null)
                {
                    return ApiResponse<string>.Faild("Enter One Image At Least");
                }
                var i = 0;
                foreach (var image in dto!.file!)
                {
                    var resultsave=await file.SaveImage(image, "ProductImages");
                    if (resultsave == null)
                    {
               return  ApiResponse<string>.Faild($"the Image Should be Less Than 2 MB");
                    }
                    var product = await productRepo.GetProductById(dto.ProductId);
                    if(product==null)
                    {
                        return ApiResponse<string>.Faild($"Product Not Found");
                    }
                    var productimage = new ProductImages()
                    {
                        Image=resultsave,
                        IsMain=i==0,
                        ProductId=dto.ProductId
                    };
                    await repo.AddImage(productimage);
                    i++;
                }

                await repo.Save();

                return ApiResponse<string>.Success("Upload Image Success");
            }
            catch(Exception ex)
            {
               return  ApiResponse<string>.Faild($"Error In save UploadImage {ex.InnerException.Message}");
            }
        }

        public async Task<ApiResponse<string>> DeleteProductImage(string ImageId)
        {
            try
            {
                if (string.IsNullOrEmpty(ImageId))
                {
                    return ApiResponse<string>.Faild("Image Not Found");
                }
                var image = await repo.GetImageById(ImageId);
                if(image==null)
                {
                    return ApiResponse<string>.Faild("Image Not Found");

                }
                var isdelete=await file.DeleteImage(image.Image);
                if (isdelete)
                {
                    await repo.DeleteImage(image);
                    await repo.Save();
                    return ApiResponse<string>.Success("success");

                }
                return ApiResponse<string>.Faild("Image Not Found");

            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Faild("Error In save UploadImage");

            }
        }

        public async Task<ApiResponse<List<ProductImageAll>>> GetAll(string ProductId)
        {
            try
            {
                if (string.IsNullOrEmpty(ProductId))
                {
                    return ApiResponse<List<ProductImageAll>>.Faild("ID Is Not Found");
                }
                var listImage = await repo.GetAll(ProductId);
                List<ProductImageAll> maplistimage = mapper.Map<List<ProductImageAll>>(listImage);
                return ApiResponse<List<ProductImageAll>>.Success(maplistimage);
            }

            catch (Exception ex)
            {
                return ApiResponse<List<ProductImageAll>>.Faild("Error In save UploadImage");

            }
        }


        public async Task<ApiResponse<string>> UpdateProductImage(UpdateImagedto dto)
        {
            try
            {
                var image = await repo.GetImageById(dto.ImageId);
                if(image==null)
                {
                    return ApiResponse<string>.Faild("Image Not Found");
                }
                await file.DeleteImage(image.Image);
                var imagesave=await file.SaveImage(dto.File!, "ProductImages");
                image.Image = imagesave;
                await repo.UpdateImage(image);
                    await repo.Save();
                return ApiResponse<string>.Success("Update Success");
            }
            catch(Exception ex)
            {
                return ApiResponse<string>.Faild("Error In save UploadImage");

            }

        }
    }
}
