using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.IRepositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomer(Guid id);
        Task<Customer> AddCustomer(Customer customer);
        Task<bool> UpdateCustomer(Guid id, Customer customer);
        Task<bool> DeleteCustomer(Guid id);
    }
}
