using EcommercePrototype.Business.IRepository;
using EcommercePrototype.Core.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace EcommercePrototype.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController: ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository productRepository, 
            ILogger<ProductController> logger)
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

        [HttpGet("{id:int}")]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Product could not be created");
            }
            var resultProductDto = await _productRepository.CreateProduct(productDto);
            if (resultProductDto == null)
            {
                return Conflict("Product could not be created");
            }
            return Ok(resultProductDto);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProduct(int id, ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Product model is not valid");
            }
            var resultProductDto = _productRepository.UpdateProduct(id, productDto);
            return Ok(resultProductDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productDto = await _productRepository.DeleteProduct(id);
            if (productDto == null)
            {
                return BadRequest("The product Dto could be found to delete");
            }
            return Ok($"Product Dto deleted successfully {productDto.Value}");
        }
    }
}
