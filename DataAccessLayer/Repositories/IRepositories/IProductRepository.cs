using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.IRepositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(Guid id);
        Task<Product> AddProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Guid id);
    }
}
