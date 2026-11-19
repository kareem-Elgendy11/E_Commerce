using Application.Dtos.CartDto;
using Application.Dtos.CategoryDto;
using Application.Dtos.OrderDto;
using Application.Dtos.ProductDto;
using Application.Dtos.ProductImageDto;
using Application.Dtos.ReviewDto;
using Application.Dtos.Roles;
using Application.Dtos.User;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapping
{
    public class Mapp:Profile
    {
        public Mapp()
        {
            CreateMap<RegisterDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserResForadminDto>();
            CreateMap<ApplicationUser,UserProfileDto>();
            CreateMap<ApplicationUser, UserResDto>();
            CreateMap<IdentityRole, RoleResDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<Product,CreateProductDto>();
            CreateMap<Product,ProductDto>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Category,AllCategoryDTO>();
            CreateMap<CreateCategoryDTO,Category>();
            CreateMap< AllCategoryDTO,Category>();
            CreateMap<UpdateQuantity, CartItem>();
            CreateMap< CartItem,ProductCart>();
            CreateMap<AddToCartDto,CartItem>();
            CreateMap<ProductImages, ProductImageAll>();
            CreateMap<createReviewdto, Reviews>();
            CreateMap<Reviews, ReviewAllDto>();
            CreateMap<Order, OrederDto>();







            //CreateMap<CreateRoleDto,RoleManager<ApplicationUser>>();
            //.ForMember(dest=>dest.email,opt=>opt.MapFrom(src=>src.Email))
            //.ForMember(des=>des.userName,opt=>opt.MapFrom(src=>src.UserName));
        }
    }
}
