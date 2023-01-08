﻿using EShopAPI.Contracts;
using EShopAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private EShopAPI_DBContext _context;
        private IMemoryCache _cache;
        public CustomerRepository(EShopAPI_DBContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
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
            var cacheCustomer =  _cache.Get<Customer>(id);
            if (cacheCustomer!=null)
            {
                return cacheCustomer;
            }
            else
            {
                var customer= await _context.Customer.Include(c => c.Orders).SingleOrDefaultAsync(c => c.CustomerId == id);
                var cacheOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));
                _cache.Set(customer.CustomerId, customer,cacheOption);
                return customer;

            }
        }

        public IEnumerable<Customer> GetAll()
        {
            return  _context.Customer.ToList();
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
