using AutoMapper;
using EcommercePrototype.API.Data.Context;
using EcommercePrototype.Business.IRepository;
using EcommercePrototype.Core.Dto;
using Microsoft.EntityFrameworkCore;

namespace EcommercePrototype.Business.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>?> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();

            if (products.Count == 0) {
                return null;
            }
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            await Console.Out.WriteLineAsync(productDtos.ToString());
            return productDtos;
        }

        public async Task<ProductDto?> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }
            return _mapper.Map<ProductDto>(product);
        }
    }
}
