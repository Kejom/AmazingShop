using AmazingShop.Database;
using AmazingShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AppUser GetById(string id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
