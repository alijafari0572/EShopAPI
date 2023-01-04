using EShopAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShopAPI.Contracts
{
    public interface IProductsRepository
    {
        IEnumerable<Products> GetAll();
        Task<Products> Find(int id);
        Task<Products> Add(Products product);
        Task<Products> Update(Products product);
        Task<Products> Remove(int id);
        Task<bool> IsExist(int id);
    }
}
