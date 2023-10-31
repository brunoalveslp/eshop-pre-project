using System.Reflection;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Data;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options): base(options)
    {   
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItem { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // if the database is sqlite it converts decimal that sqlite don't support for double
        if(Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            foreach(var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties()
                                        .Where(p => p.PropertyType == typeof(decimal));

                foreach(var property in properties)
                {
                    modelBuilder.Entity(entityType.Name).Property(property.Name)
                                                            .HasConversion<double>();
                }
            }
        }
    }
}
