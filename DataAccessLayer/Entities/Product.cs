using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public bool IsActive { get; set; }
    }
}
