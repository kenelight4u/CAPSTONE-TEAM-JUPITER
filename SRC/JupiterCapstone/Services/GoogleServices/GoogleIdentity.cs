using JupiterCapstone.DTO;
using JupiterCapstone.Models;
using JupiterCapstone.Services.GoogleServices.IGoogleService;
using JupiterCapstone.Services.IService;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Services.GoogleServices
{
    public class GoogleIdentity : IGoogleIdentity
    {
        private readonly UserManager<User> _userManager;

        public GoogleIdentity(UserManager<User> userManager)
        {
            _userManager = userManager;           
        }

        public async Task<User> GetOrCreateExternalLoginUser(string provider, string key, string email, string firstName, string lastName)
        {
            // Login already linked to a user
            var user = await _userManager.FindByLoginAsync(provider, key);
            if (user != null)
                return user;

            user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // No user exists with this email address, we create a new one
                user = new User
                {
                    Email = email,
                    UserName = email,
                    FirstName = firstName,
                    LastName = lastName
                };

                await _userManager.CreateAsync(user);
            }

            // Link the user to this login
            var info = new UserLoginInfo(provider, key, provider.ToUpperInvariant());
            var result = await _userManager.AddLoginAsync(user, info);
            if (result.Succeeded)
                return user;

            //_logger.LogError("Failed add a user linked to a login.");
            //_logger.LogError(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            return null;
        }

        
    }
}
