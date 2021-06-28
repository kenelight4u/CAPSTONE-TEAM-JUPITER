using JupiterCapstone.Data;
using JupiterCapstone.Models;
using JupiterCapstone.Services.IService;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly UserManager<User> _userManager;

        public UserService(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        
        public User GetUser(string id)
        {
            return _userManager.Users.FirstOrDefault(a => a.Id == id);
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public bool UpdateUser(User model)
        {
            _dbContext.Users.Update(model);
            return Save();
        }
    }
}
