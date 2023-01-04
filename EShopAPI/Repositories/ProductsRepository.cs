using EShopAPI.Contracts;
using EShopAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShopAPI.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private EShopAPI_DBContext _context;

        public ProductsRepository(EShopAPI_DBContext context)
        {
            _context = context;
        }
        public async Task<Products> Add(Products product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Products> Find(int id)
        {
            return await _context.Products.Include(p => p.OrderItems)
                .SingleOrDefaultAsync(p => p.ProductsId == id);
        }

        public IEnumerable<Products> GetAll()
        {
            return _context.Products;
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.Products.AnyAsync(c => c.ProductsId == id);
        }

        public async Task<Products> Remove(int id)
        {
            var product = await _context.Products.SingleAsync(p => p.ProductsId == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Products> Update(Products product)
        {
             _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;

        }
    }
}
