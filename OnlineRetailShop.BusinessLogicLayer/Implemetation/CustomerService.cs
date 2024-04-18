
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.IRepositories;
using LazyCache;
using Microsoft.Extensions.Caching.Memory;
using OnlineRetailShop.BusinessLogicLayer.Caching;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implemetation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMemoryCache _memoryCache;

         public CustomerService(ICustomerRepository customerRepository,IMemoryCache memoryCache)
        {
            _customerRepository = customerRepository;
            _memoryCache = memoryCache;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _customerRepository.GetCustomers();
            
        }

        public async Task<Customer> GetCustomerById(Guid id)
        {
            var customer =  await _customerRepository.GetCustomer(id);
            return customer;
        }

        public async Task<Customer> PostCustomer(Customer customer)
        {
            return await _customerRepository.AddCustomer(customer);
        }
        public async Task<bool> PutCustomer(Guid id,Customer customer)
        {
            var result = await _customerRepository.UpdateCustomer(id, customer);
            if (!result)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> DeleteCustomer(Guid id)
        {
            var result = await _customerRepository.DeleteCustomer(id);
            if (!result)
            {
                return false;
            }
            return true;
        }

        public async Task<List<Customer>> GetCustomerCache()
        {
            if (_memoryCache.TryGetValue(CacheKeys.Customer, out List<Customer> customers))
                return customers;

            customers = await _customerRepository.GetCustomers();

            var cacheEntryOption = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(30),
                Size = 1024
            };
            _memoryCache.Set(CacheKeys.Customer, customers, cacheEntryOption);


            return customers;

        }
    }
}
