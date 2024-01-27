using EcommercePrototype.Business.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace EcommercePrototype.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController: ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProducts()
        {
            _logger.LogInformation("Fetching request to get all products");

            var products = await _productRepository.GetAllProducts();
            if (products == null)
            {
                _logger.LogInformation("No product fetched from database");
                return Ok("No Product available");
            }
            _logger.LogInformation($"{products.Count()} products fetched from database");

            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProduct(int id)
        {
            _logger.LogInformation($"Request to fetch the product with id = {id}");
            if (id == 0)
            {
                return BadRequest("Invalid product id");
            }
            var product = await _productRepository.GetProductById(id);

            if (product == null)
            {
                _logger.LogInformation($"product does not exist for id = {id}");
                return Ok("Product does not exist");
            }
            _logger.LogInformation($"product fetched for id = {id}");
            return Ok(product);
        }
    }
}
