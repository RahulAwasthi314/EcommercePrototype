using EcommercePrototype.API.Data.Context;
using EcommercePrototype.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace EcommercePrototype.DataAccess.Data.Context
{
    public static class ContextSeed
    {
        public static void ApplyMigration(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
                var loggerFactory = scope.ServiceProvider.GetService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("");
                if (logger == null)
                {
                    throw new Exception("Logger cannot be initialized");
                }
                try
                {
                    if (db == null)
                    {
                        throw new ArgumentNullException(nameof(db));
                    }
                    int pendingMigrationCount = db.Database.GetPendingMigrations().Count();
                    if (pendingMigrationCount > 0)
                    {
                        logger.LogInformation($"Migration pending migrations: {pendingMigrationCount}");
                        db.Database.Migrate();
                    }
                } 
                catch(Exception ex)
                {
                    logger.LogError(ex, "Exception occurred");
                }
            }
        }

        public static void ProductSeedAsync(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
                var loggerFactory = scope.ServiceProvider.GetService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("");
                if (logger == null)
                {
                    throw new Exception("Logger cannot be initialized");
                }
                try
                {
                    if (db == null)
                    {
                        throw new ArgumentNullException(nameof(db));
                    }
                    if (!db.Products.Any())
                    {
                        db.Products.AddRange(products);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Exception occurred");
                }
            }
        }

        private static List<Product> products = new List<Product>()
        {
            new Product
            {
                Name = "Polo Tshirt",
                Description = "Polo tshirts are best to wear at summer. Provides sweatfree days in hot days",
                Price = 100
            },
            new Product
            {
                Name = "Formal Shirt",
                Description = "Formal shirts are all weather shirts that can be used for daily office fashion",
                Price = 300
            },
            new Product
            {
                Name = "Sport shoes",
                Description = "The shoes that you can wear while playing sports, walking, running or jogging",
                Price = 600
            },
            new Product
            {
                Name = "Eye protecting glasses",
                Description = "Prmosie your eyes to protect them from dust and dirt",
                Price = 100
            }
        };
    }
}
