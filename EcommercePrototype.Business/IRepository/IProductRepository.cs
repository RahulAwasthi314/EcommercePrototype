using EcommercePrototype.Core.Dto;

namespace EcommercePrototype.Business.IRepository
{
    public interface IProductRepository
    {
        public Task<IEnumerable<ProductDto>?> GetAllProducts();
        public Task<ProductDto?> GetProductById(int id);
    }
}
