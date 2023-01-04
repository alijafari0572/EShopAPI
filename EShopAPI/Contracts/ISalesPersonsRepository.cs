using EShopAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShopAPI.Contracts
{
    public interface ISalesPersonsRepository
    {
        IEnumerable<SalesPersons> GetAll();
        Task<SalesPersons> Find(int id);
        Task<SalesPersons> Add(SalesPersons salesPersons);
        Task<SalesPersons> Update(SalesPersons salesPersons);
        Task<SalesPersons> Remove(int id);
        Task<bool> IsExist(int id);
    }
}
