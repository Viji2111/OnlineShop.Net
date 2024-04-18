using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CommonContext _dbContext;

        public CustomerRepository(CommonContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Update references from 'customer' to 'Customer'
        public async Task<List<Customer>> GetCustomers()
        {
            return _dbContext.customers.ToList();
        }

        public async Task<Customer> GetCustomer(Guid id)
        {
            return await _dbContext.customers.FindAsync(id);
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            customer.CustomerId = Guid.NewGuid();
            _dbContext.customers.Add(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> UpdateCustomer(Guid id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return false;
            }

            _dbContext.Entry(customer).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public async Task<bool> DeleteCustomer(Guid id)
        {
            var customer = await _dbContext.customers.FindAsync(id);
            if (customer == null)
            {
                return false;
            }

            _dbContext.customers.Remove(customer);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        private bool CustomerExists(Guid id)
        {
            return _dbContext.customers.Any(x => x.CustomerId == id);
        }
    }
}
