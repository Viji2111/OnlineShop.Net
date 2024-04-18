using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }

        public string? CustomerName { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
    }
}

