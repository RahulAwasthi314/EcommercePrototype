using EcommercePrototype.API.Data.Context;
using EcommercePrototype.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;


namespace EcommercePrototype.DataAccess.Data.Context
{
    public static class ContextSeed
    {
        public static void ProductSeedAsync(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
                if (db != null && db.Database.GetPendingMigrations().Count() > 0)
                {
                    db.Database.Migrate();
                }
            }
        }

        public static void ApplyMigration(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
                if (db != null)
                {
                    db.Products.AddRange(products);
                    db.SaveChanges();
                }
            }
        }

        private static List<Product> products = new List<Product>()
        {
            new Product
            {
                Id = 1,
                Name = "Polo Tshirt",
                Description = "Polo tshirts are best to wear at summer. Provides sweatfree days in hot days",
                Price = 100
            },
            new Product
            {
                Id = 2,
                Name = "Formal Shirt",
                Description = "Formal shirts are all weather shirts that can be used for daily office fashion",
                Price = 300
            },
            new Product
            {
                Id = 3,
                Name = "Sport shoes",
                Description = "The shoes that you can wear while playing sports, walking, running or jogging",
                Price = 600
            },
            new Product
            {
                Id = 4,
                Name = "Eye protecting glasses",
                Description = "Prmosie your eyes to protect them from dust and dirt",
                Price = 100
            }
        };
    }
}
