using EShopAPI.Contracts;
using EShopAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShopAPI.Repositories
{
    public class SalesPersonsRepository : ISalesPersonsRepository
    {
        private EShopAPI_DBContext _context;

        public SalesPersonsRepository(EShopAPI_DBContext context)
        {
            _context = context;
        }
        public async Task<SalesPersons> Add(SalesPersons salesPersons)
        {
            await _context.SalesPersons.AddAsync(salesPersons);
            await _context.SaveChangesAsync();
            return salesPersons;
        }

        public async Task<SalesPersons> Find(int id)
        {
            return await _context.SalesPersons.Include(s=>s.Orders)
                .SingleOrDefaultAsync(s => s.SalesPersonId == id);
        }

        public IEnumerable<SalesPersons> GetAll()
        {
            return _context.SalesPersons;
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.SalesPersons.AnyAsync(s => s.SalesPersonId == id);
        }

        public async Task<SalesPersons> Remove(int id)
        {
            var salesPerson = await _context.SalesPersons.SingleAsync(s => s.SalesPersonId == id);
            _context.SalesPersons.Remove(salesPerson);
            await _context.SaveChangesAsync();
            return salesPerson;
        }

        public async Task<SalesPersons> Update(SalesPersons salesPersons)
        {
            _context.SalesPersons.Update(salesPersons);
            await _context.SaveChangesAsync();
            return salesPersons;
        }
    }
}
