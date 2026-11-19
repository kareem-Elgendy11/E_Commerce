using Application.Interfaces.IServices;
using Application.Interfaces.Reposatoriese;
using Application.Services;
using Infrastructure.Reposatories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DependencyInjection
{
    public static class DependencyInjectinInfra
    {
        public static IServiceCollection AddDependencyServiceinfra(this IServiceCollection service)
        {
            service.AddScoped<IRefreshTokenRepo,RefreshRepo>();
            service.AddScoped<IUserRepo, UserRepo>();
            service.AddScoped<IProductRepo, ProductRepo>();
            service.AddScoped<ICategoryRepo, CategoryRepo>();
            service.AddScoped<ICartRepo, CartRepo>();
            service.AddScoped<IProductImageRepo, ProductImagesRepo>();
            service.AddScoped<IReviewRepo, ReviewRepo>();
            service.AddScoped<IOrderRepo, OrderRepo>();




            return service;
        }
    }
}
