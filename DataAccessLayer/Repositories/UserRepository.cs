using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CommonContext _context;
        public UserRepository(CommonContext context)
        {
            _context = context;
        }
        public async Task<UserCredentials> GetUser(int userid)
        {
            UserCredentials user = null;
            user = _context.UserCredentials.Find(userid);
            return user;
        }
    }
}
