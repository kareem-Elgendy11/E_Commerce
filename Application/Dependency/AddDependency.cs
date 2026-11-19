using Api;
using Application.Interfaces.IServices;
using Application.Interfaces.Reposatoriese;
using Application.Mapping;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dependency
{
    public static class AddDependency
    {
        public static IServiceCollection AddDependencyService(this IServiceCollection service)
        {
            service.AddScoped<IAuth, Auth>();
            service.AddScoped<IsendEmail,SendEmail>();
            service.AddScoped<ITokenServices, TokenServices>();
            service.AddScoped<IUser,UserService>();
            service.AddScoped<IFileServices, FileServices>();
            service.AddScoped<IRoleService, RoleService>();
            service.AddScoped<IProductservice, ProductServics>();
            service.AddScoped<ICategoryService,CategoryServices>();
            service.AddScoped<ICartService, CartService>();
            service.AddScoped<IProductImageService, ProductImagesService>();
            service.AddScoped<IReviewService, ReviewService>();
            service.AddScoped<IOrderService, OrderService>();





            service.AddAutoMapper(m=>m.AddMaps(typeof(Mapp).Assembly));
            return service;
        }
    }
}
