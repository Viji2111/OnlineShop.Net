using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.IRepositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(Guid id);
        Task<Order> AddOrder(Order order);
        Task<bool> UpdateOrder(Order order);


        Task<bool> DeleteOrder(Guid id);

        Task<Product> MostSoldProduct();
    }
}
