using EShopAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShopAPI.Contracts
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        Task<Customer> Find(int id);
        Task<Customer> Add(Customer customer);
        Task<Customer> Update(Customer customer);
        Task<Customer> Remove(int id);
        Task<bool> IsExist(int id);
        //Task<int> CountCustomer();
        int CountCustomer();

    }
}
