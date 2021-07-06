using JupiterCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.IService
{
    public interface IShippingAddressService
    {
        bool SaveChanges();

        IEnumerable<UsersAddress> GetAddressByUserId(string id);

        void AddAddress(UsersAddress address);

        void DeleteUserAddresse(string userId, List<string> addressIdToDelete);
    }
}
