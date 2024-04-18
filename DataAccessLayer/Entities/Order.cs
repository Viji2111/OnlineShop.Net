using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        [Required]
        [ForeignKey("customer")]

        public Guid CustomerId { get; set; }
        [Required]
        [ForeignKey("Product")]

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public bool IsCancel { get; set; }
    }
}
