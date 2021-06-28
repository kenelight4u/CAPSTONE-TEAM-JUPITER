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

        public async Task<User> GetOrCreateExternalLoginUser(GoogleLoginRequest googleModel)
        {
            // Login already linked to a user
            var user = await _userManager.FindByLoginAsync(googleModel.Provider, googleModel.Key);
            if (user != null)
                return user;

            user = await _userManager.FindByEmailAsync(googleModel.Email);
            if (user == null)
            {
                // No user exists with this email address, we create a new one
                user = new User
                {
                    Email = googleModel.Email,
                    UserName = googleModel.Email,
                    FirstName = googleModel.FirstName,
                    LastName = googleModel.LastName
                };

                await _userManager.CreateAsync(user);
            }

            // Link the user to this login
            var info = new UserLoginInfo(googleModel.Provider, googleModel.Key, googleModel.Provider.ToUpperInvariant());
            var result = await _userManager.AddLoginAsync(user, info);
            if (result.Succeeded)
                return user;

            //_logger.LogError("Failed add a user linked to a login.");
            //_logger.LogError(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            return null;
        }

        
    }
}
