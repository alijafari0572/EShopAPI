using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using WebClient.Models;

namespace WebClient.Repositores
{
    public class CustomerRepository
    {
        private string apiURL = "http://localhost:40116/api/Customers";
        private HttpClient _client;

        public CustomerRepository()
        {
            _client = new HttpClient();
        }
        public List<Customer> GetAllCustomer()
        {
            var result = _client.GetStringAsync(apiURL).Result;

            List<Customer> list = JsonConvert.DeserializeObject<List<Customer>>(result);

            return list;
        }

        public Customer GetCustomerById(int customerid)
        {
            var result = _client.GetStringAsync(apiURL + "/" + customerid).Result;
            Customer customer = JsonConvert.DeserializeObject<Customer>(result);
            return customer;
        }
        public void AddCustomer(Customer customer)
        {
            string jsoncustomer = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(jsoncustomer, Encoding.UTF8, "application/json");
            var result = _client.PostAsync(apiURL, content).Result;
        }
        public void UpdateCustomer(Customer customer)
        {
            string jsoncustomer = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(jsoncustomer, Encoding.UTF8, "application/json");
            var result = _client.PutAsync(apiURL+"/"+customer.CustomerId, content).Result;
        }
        public void DeletCustomer(int customerId)
        {
            var result = _client.DeleteAsync(apiURL + "/" + customerId).Result;
        }
    }
}
