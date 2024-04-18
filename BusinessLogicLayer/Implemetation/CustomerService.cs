using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implemetation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

         public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
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

    }
}
