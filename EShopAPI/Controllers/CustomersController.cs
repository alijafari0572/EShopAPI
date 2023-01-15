using EShopAPI.Contracts;
using EShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CustomersController : ControllerBase
    {
        private ICustomerRepository _customerRepository;
        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        /// <summary>
        /// this method
        /// </summary>
        /// <returns>take all</returns>
        [HttpGet]
        //[ResponseCache(Duration =60)]
      
        public IActionResult GetCustomer()
        {
            var result = new ObjectResult(_customerRepository.GetAll())
            {
                StatusCode = (int)HttpStatusCode.OK
            };
            Request.HttpContext.Response.Headers.Add("count", _customerRepository.CountCustomer().ToString());
            return result;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if (await CustomerExist(id))
            {
                var customer = await _customerRepository.Find(id);
                return Ok(customer);
            }
            else return NotFound();

        }
        //[HttpGet("ali",Name ="ali")]
        //public IActionResult GetAli()
        //{
        //    return Content("Alijafari");
        //}
        private async Task<bool> CustomerExist(int id)
        {
            return await _customerRepository.IsExist(id);
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(customer);
            }

            await _customerRepository.Add(customer);
            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, Customer customer)
        {
            await _customerRepository.Update(customer);
            return Ok(customer);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletCustomer([FromRoute] int id)
        {
            await _customerRepository.Remove(id);
            return Ok();
        }
    }
}
