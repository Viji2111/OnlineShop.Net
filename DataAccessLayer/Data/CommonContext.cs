using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Data
{
    public class CommonContext : DbContext
    {
        public CommonContext(DbContextOptions options ): base(options) 
        { 

        }

        public DbSet<Customer> customers { get; set; }

        public DbSet <Product> Products { get; set; }

        public DbSet<Order>   Orders { get; set; }

        public DbSet<UserCredentials> UserCredentials { get; set; }
    }
}
