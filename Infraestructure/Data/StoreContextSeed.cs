
using System.Text.Json;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infraestructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if(!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Infraestructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrands.AddRange(brands);
            }

            if(!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../Infraestructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);
            }

            if(!context.Products.Any())
            {
                var productsData = File.ReadAllText("../Infraestructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
            }
            
            if(!context.DeliveryMethods.Any())
            {
                var deliverysData = File.ReadAllText("../Infraestructure/Data/SeedData/delivery.json");
                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliverysData);

                context.DeliveryMethods.AddRange(methods);
            }

            if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            var logger = loggerFactory.CreateLogger<StoreContextSeed>();
            logger.LogError(ex.Message + " An Error ocurred while seeding data to database.");
        }
    }
}