using EcommercePrototype.Business.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace EcommercePrototype.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController: ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAllProducts();
            if (products == null)
            {
                return Ok("No Product available");
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid product id");
            }
            var product = await _productRepository.GetProductById(id);

            if (product == null)
            {
                return Ok("Product does not exist");
            }
            return Ok(product);
        }
    }
}
