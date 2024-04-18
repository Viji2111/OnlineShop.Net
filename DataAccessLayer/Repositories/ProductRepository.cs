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
    public class ProductRepository : IProductRepository
    {
        private readonly CommonContext _dbContext;

        public ProductRepository(CommonContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<Product> AddProduct(Product product)
        {
            var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductName == product.ProductName);
            if (existingProduct != null)
            {
                throw new InvalidOperationException("Product already exists");
            }

            product.ProductId = Guid.NewGuid();
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;

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

        public async Task<bool> DeleteProduct(Guid id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
