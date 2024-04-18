using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.IRepositories;
using OnlineRetailShop.BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRetailShop.BusinessLogicLayer.Implemetation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository productRepository) 
        {
            _repository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetProduct()
        {
            return await _repository.GetAllProducts();
        }
        public async Task<Product> GetProductById(Guid id)
        {
            return await _repository.GetProductById(id);
        }

        public async Task<Product> PostProduct(Product product)
        {
            return await _repository.AddProduct(product);
        }
        public async Task<bool> PutProduct(Product product)
        {
            var products =  _repository.UpdateProduct(product);
            if(products != null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            var result = await _repository.DeleteProduct(id);
            if(result != null)
            {
                return false;
            }
            return true;
        }
    }
}
