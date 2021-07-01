using JupiterCapstone.DTO;
using JupiterCapstone.Models;
using JupiterCapstone.Services.AuthorizationServices;
using JupiterCapstone.Services.GoogleServices.IGoogleService;
using JupiterCapstone.Services.IService;
using JupiterCapstone.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace JupiterCapstone.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        private readonly UserManager<User> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IGoogleIdentity _googleIdentity;

        public AuthenticationController(IIdentityService identityService, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IGoogleIdentity googleIdentity)
        {
            _identityService = identityService;
            _userManager = userManager;
            _roleManager = roleManager;
            _googleIdentity = googleIdentity;
        }
        
        [HttpPost]
        [Route("/logIn")]
        public async Task<IActionResult> LoginAsync([FromBody] LogIn login)
        {
            var result = await _identityService.LoginAsync(login);
            return Ok(result);
        }
        
        [HttpPost]
        [Route("/refresh-token")]
        public async Task<IActionResult> Refresh([FromBody] TokenModel request)
        {
            var result = await _identityService.RefreshTokenAsync(request);
            return Ok(result);
        }

        //Remember to shorten the code to a service class
        [HttpPost]
        [Route("/register-user")]
        public async Task<IActionResult> RegisterAsync([FromBody] Register model)
        {
            var emailExists = await _userManager.FindByEmailAsync(model.Email);

            if (emailExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            User user = new User()
            {
                UserName = model.Email,
                Email = model.Email,
                PasswordHash = new PasswordHasher<object>().HashPassword(null, model.Password),
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("/register-admin")]
        public async Task<IActionResult> RegisterAdminAsync([FromBody] Register model)
        {
            var emailExists = await _userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            User user = new User()
            {
                UserName = model.Email,
                Email = model.Email,
                PasswordHash = new PasswordHasher<object>().HashPassword(null, model.Password),
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        //Not tested yet...
        [HttpPost]
        [Route("/google-signIn")]
        [ProducesDefaultResponseType]
        public async Task<JsonResult> GoogleLogin(GoogleLoginRequest request)
        {
            _ = new Payload();
            Payload payload;
            try
            {
                payload = await ValidateAsync(request.IdToken, new ValidationSettings
                {
                    Audience = new[] { "806516260930-fav0u51tnpsfdrn6vamfb9gi9fidvilh.apps.googleusercontent.com" }
                });
                // It is important to add your ClientId as an audience in order to make sure
                // that the token is for your application!
            }
            catch (Exception ex)
            {
                // Invalid token
                // return StatusCode(500, ex.Message);
                return new JsonResult(500, ex.Message);
            }

            var user = await _googleIdentity.GetOrCreateExternalLoginUser("google", payload.Subject, payload.Email, payload.GivenName, payload.FamilyName);

            var token = await _identityService.GenerateToken(user);

            return new JsonResult(token);

           
        }
    }
}
