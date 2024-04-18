using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRetailShop.BusinessLogicLayer.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProduct();

        Task<Product> GetProductById(Guid id);

        Task<Product> PostProduct(Product product);

        Task<bool> PutProduct(Product product);

        Task<bool> DeleteProduct(Guid id);


    }
}
