using Project.Core.Entities;
using Project.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.Repository.Data
{
    public static class StoreDbContextSeed
    {
        public async static Task SeedAsync( AppDbContext _context ) 
        {

            // If Table Brands Is Empty
            if (_context.Brands.Count() == 0) 
            {
                //  (1) Read Data From Json File
                var brandsData = File.ReadAllText(@"..\Project.Repository\Data\DataSeed\brands.json");

                //  (2) Convert Jason To List<T>
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);


                //  (3) Seed Data To Database
                if (brands is not null && brands.Count > 0)
                {
                    await _context.Brands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();
                }
            }

            // If Table Types Is Empty
            if (_context.Types.Count() == 0)
            {
                //  (1) Read Data From Json File
                var typesData = File.ReadAllText(@"..\Project.Repository\Data\DataSeed\types.json");

                //  (2) Convert Jason To List<T>
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);


                //  (3) Seed Data To Database
                if (types is not null && types.Count > 0)
                {
                    await _context.Types.AddRangeAsync(types);
                    await _context.SaveChangesAsync();
                }
            }

            // If Table Products Is Empty
            if (_context.Products.Count() == 0)
            {
                //  (1) Read Data From Json File
                var productsData = File.ReadAllText(@"..\Project.Repository\Data\DataSeed\products.json");

                //  (2) Convert Jason To List<T>
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);


                //  (3) Seed Data To Database
                if (products is not null && products.Count > 0)
                {
                    await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();
                }
            }
        }

    }
}
