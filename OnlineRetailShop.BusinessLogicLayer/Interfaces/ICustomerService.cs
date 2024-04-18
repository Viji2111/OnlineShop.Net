using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>>GetCustomers();
        Task<Customer>GetCustomerById(Guid id);

        Task<List<Customer>> GetCustomerCache();

        Task<Customer> PostCustomer(Customer customer);

        Task<bool> PutCustomer(Guid id,Customer customer);

        Task<bool> DeleteCustomer(Guid id);
    }
}
