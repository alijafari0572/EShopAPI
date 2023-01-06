using EShopAPI.Contracts;
using EShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductsRepository _productRepository;

        public ProductsController(IProductsRepository productRepository)
        {
            _productRepository = productRepository;
        }



        // GET: api/Products
        [HttpGet]
        public IEnumerable<Products> GetProducts()
        {
            return _productRepository.GetAll();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products = await _productRepository.Find(id);

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts([FromRoute] int id, [FromBody] Products products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != products.ProductsId)
            {
                return BadRequest();
            }

            await _productRepository.Update(products);



            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProducts([FromBody] Products products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productRepository.Add(products);

            return CreatedAtAction("GetProducts", new { id = products.ProductsId }, products);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products = await _productRepository.Find(id);
            if (products == null)
            {
                return NotFound();
            }

            await _productRepository.Remove(id);

            return Ok(products);
        }

        private async Task<bool> ProductsExists(int id)
        {
            return await _productRepository.IsExist(id);
        }
    }
}
