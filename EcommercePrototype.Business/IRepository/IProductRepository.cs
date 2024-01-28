using EcommercePrototype.Core.Dto;

namespace EcommercePrototype.Business.IRepository
{
    public interface IProductRepository
    {
        public Task<IEnumerable<ProductDto>?> GetAllProducts();
        public Task<ProductDto?> GetProductById(int id);
        public Task<ProductDto?> CreateProduct(ProductDto productDto);
        public ProductDto? UpdateProduct(int id, ProductDto productDto);
        public Task<ProductDto?> DeleteProduct(int id);
    }
}
