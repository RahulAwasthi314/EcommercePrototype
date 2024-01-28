using AutoMapper;
using EcommercePrototype.API.Data.Context;
using EcommercePrototype.Business.IRepository;
using EcommercePrototype.Core.Dto;
using EcommercePrototype.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EcommercePrototype.Business.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ApplicationDbContext context,
            IMapper mapper, ILogger<ProductRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductDto?> CreateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return productDto;
        }

        public async Task<ProductDto?> DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return null;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>?> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();

            if (products.Count == 0) {
                _logger.LogInformation("No product found in database");
                return null;
            }

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            _logger.LogInformation($"{products.Count()} products fetched from database");
            return productDtos;
        }

        public async Task<ProductDto?> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                _logger.LogInformation($"No product found for id = {id} in database");
                return null;
            }
            _logger.LogInformation($"{product.Name} products fetched from database");

            return _mapper.Map<ProductDto>(product);
        }

        public ProductDto? UpdateProduct(int id, ProductDto productDto)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                // change the product parameters
                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Price = productDto.Price;
                _context.Products.Update(product);
                _context.SaveChanges();
            }
            return _mapper.Map<ProductDto>(product);
        }
    }
}
