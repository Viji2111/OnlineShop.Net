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
    public class OrderRepository : IOrderRepository
    {
        private readonly CommonContext _dbContext;

        public OrderRepository(CommonContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            return await _dbContext.Orders.FindAsync(id);
        }

        public async Task<Order> AddOrder(Order order)
        {
            var product = await _dbContext.Products.FindAsync(order.ProductId);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found");
            }

            if (product.Quantity < order.Quantity)
            {
                throw new InvalidOperationException("Insufficient quantity available");
            }

            order.OrderId = Guid.NewGuid();
            _dbContext.Orders.Add(order);
            product.Quantity -= order.Quantity;
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }


        public async Task<bool> DeleteOrder(Guid id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<Product> MostSoldProduct()
        {
            var productSales = await _dbContext.Orders
                .GroupBy(pi => pi.ProductId)
                .Select(s => new
                {
                    ProductId = s.Key,
                    TotalSales = s.Sum(q => q.Quantity)
                }).ToListAsync();

            var maxSalesProduct = productSales.OrderByDescending(ps => ps.TotalSales).FirstOrDefault();

            if (maxSalesProduct == null)
                return null;

            var mostSoldProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == maxSalesProduct.ProductId);
            return mostSoldProduct;
        }
    }
}

