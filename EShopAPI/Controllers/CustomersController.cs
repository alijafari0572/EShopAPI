using EShopAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private EShopAPI_DBContext _context;

        public CustomersController(EShopAPI_DBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetCustomer()
        {
            return new ObjectResult(_context.Customer);
        }
        [HttpGet("{id}")]
        public async  Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            var customer =await _context.Customer.SingleOrDefaultAsync(c => c.CustomerId == id);
            return Ok(customer);
        }
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute]int id,Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(customer);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletCustomer([FromRoute] int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
