using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
    public interface IUserService
    {
        
        User GetUser(string id);

        bool UpdateUser(User model);

        bool Save();
    }
}
