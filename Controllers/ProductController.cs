using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeaStoreApi.Interfaces;
using TeaStoreApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeaStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> Get(string productType, int? categoryId = null)
        {
            var products = await _productRepository.GetProducts(productType, categoryId);
            if(products.Any())
            {
                return Ok(products);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        // POST api/<ProductController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Product product)
        {
            var isAdded = await _productRepository.AddProduct(product);
            if (isAdded) 
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest();
        }

        // PUT api/<ProductController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            var isUpdated = await _productRepository.UpdateProduct(id, product);
            if (isUpdated) 
            { 
                return Ok(); 
            }
            return BadRequest();
        }

        // DELETE api/<ProductController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _productRepository.DeleteProduct(id);
            if (isDeleted) 
            {
                return Ok(); 
            }
            return BadRequest();
        }
    }
}
