using JupiterCapstone.Data;
using JupiterCapstone.Models;
using JupiterCapstone.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services
{
    public class ShippingAddressService : IShippingAddressService
    {
        private readonly ApplicationDbContext _context;
        public ShippingAddressService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddAddress(UsersAddress address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            _context.UsersAddresses.Add(address);
        }

        public IEnumerable<UsersAddress> GetAddressByUserId(string id)
        {
            return _context.UsersAddresses.ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        //public void DeleteAddress(UsersAddress address)
        //{
        //    if (address == null)
        //        throw new ArgumentNullException(nameof(address));

        //    _context.UsersAddresses.Remove(address);
        //}

       
        public void DeleteUserAddresse( string userId, List<string> addressIdToDelete)
        {
            List<UsersAddress> userAddress = new List<UsersAddress>();
            userAddress = _context.UsersAddresses.Where(c => c.UserId == userId && addressIdToDelete.Contains(c.Id)).ToList();

            if (userAddress.Count() != 0)
            {
                _context.UsersAddresses.RemoveRange(userAddress);
                _context.SaveChanges();
            }

        }
    }
}
