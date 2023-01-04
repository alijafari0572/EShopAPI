using EShopAPI.Contracts;
using EShopAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private EShopAPI_DBContext _context;

        public CustomerRepository(EShopAPI_DBContext context)
        {
            _context = context;
        }

        public async Task<Customer> Add(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        //public async Task<int> CountCustomer()
        //{
        //    return await _context.Customer.CountAsync();
        //}
        public int CountCustomer()
        {
            return  _context.Customer.Count();
        }

        public async Task<Customer> Find(int id)
        {
            return await _context.Customer.Include(c=>c.Orders).SingleOrDefaultAsync(c=>c.CustomerId==id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customer.ToList();
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.Customer.AnyAsync(c => c.CustomerId == id);
        }

        public async Task<Customer> Remove(int id)
        {
            var customer = await _context.Customer.SingleAsync(c => c.CustomerId == id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Update(Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}
