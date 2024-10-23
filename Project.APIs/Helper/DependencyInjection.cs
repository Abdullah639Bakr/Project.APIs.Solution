﻿using Microsoft.EntityFrameworkCore;
using Project.Core.Services.Contract;
using Project.Core;
using Project.Repository;
using Project.Repository.Data.Contexts;
using Project.Service.Services.Products;
using Project.Core.Mapping.Products;
using Microsoft.AspNetCore.Mvc;
using Project.APIs.Errors;
using Project.Core.Repositories.Contract;
using Project.Repository.Repositories;
using StackExchange.Redis;
using Project.Core.Mapping.Baskets;
using Project.Service.Services.Cashes;
using Project.Repository.Identity.Contexts;
using Project.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Project.Service.Services.Tokens;
using Project.Service.Services.Users;

namespace Project.APIs.Helper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddBuiltInService();
            services.AddSwaggerService();
            services.AddDbContextService(configuration);
            services.AddUserDefinedService();
            services.AddAutoMapperService(configuration);
            services.ConfigureInvalidModelStateResponseService();
            services.AddRedisService(configuration);
            services.AddIdentityService();

            return services;
        }
        private static IServiceCollection AddBuiltInService(this IServiceCollection services) 
        {
            services.AddControllers();
            return services;
        }

        private static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        private static IServiceCollection AddDbContextService(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<StoreIdentityDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });
            return services;
        }

        private static IServiceCollection AddUserDefinedService(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICasheService,CasheService>();
            services.AddScoped<ITokenService,TokenService>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            return services;
        }

        private static IServiceCollection AddAutoMapperService(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddAutoMapper(m => m.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new BasketProfile()));

            return services;
        }

        private static IServiceCollection ConfigureInvalidModelStateResponseService(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                             .SelectMany(p => p.Value.Errors)
                                             .Select(E => E.ErrorMessage)
                                             .ToArray();
                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors

                    };
                    return new BadRequestObjectResult(response);
                };
            });
            return services;
        }

        private static IServiceCollection AddRedisService(this IServiceCollection services , IConfiguration configuration)
        {

            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection =  configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);

            });
            
            return services;
        }

        private static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDbContext>();
            return services;
        }


    }
}
