using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class UserCredentials
    {
        [Key]
        public int UserId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
