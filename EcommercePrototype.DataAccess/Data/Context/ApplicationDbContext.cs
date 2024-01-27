using EcommercePrototype.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace EcommercePrototype.API.Data.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price).HasPrecision(10, 2);

            // Add data while applying migration to database
            modelBuilder.Entity<Product>().HasData(products);
        }

        private List<Product> products = new List<Product>()
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
