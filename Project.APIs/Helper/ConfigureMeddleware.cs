using Project.APIs.Middlewares;
using Project.Repository.Data.Contexts;
using Project.Repository.Data;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace Project.APIs.Helper
{
    public  static class ConfigureMeddleware
    {
        public static async Task<WebApplication> ConfigureMeddlewareAsync(this WebApplication app) 
        {


            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AppDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                //// Create Database
                await context.Database.MigrateAsync();
                //// SeedingData
                await StoreDbContextSeed.SeedAsync(context);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "There Are Proplems During Apply Migrations !");
            }

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }
    }
}
