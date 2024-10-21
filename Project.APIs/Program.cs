
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.APIs.Errors;
using Project.APIs.Middlewares;
using Project.Core;
using Project.Core.Mapping.Products;
using Project.Core.Services.Contract;
using Project.Repository;
using Project.Repository.Data;
using Project.Repository.Data.Contexts;
using Project.Service.Services.Products;
using Project.APIs.Helper;

namespace Project.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container....
            builder.Services.AddDependency(builder.Configuration);
            var app = builder.Build();
            await app.ConfigureMeddlewareAsync();
            app.Run();
        }
    }
}
