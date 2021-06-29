using AutoMapper;
using JupiterCapstone.DTO;
using JupiterCapstone.Models;
using JupiterCapstone.SendGrid;
using JupiterCapstone.Services.IService;
using JupiterCapstone.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace JupiterCapstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        private readonly IMapper _mapper;

        private readonly UserManager<User> _userManager;

        private readonly IMailService _mailService;

        public UserController(IUserService service, IMapper mapper, UserManager<User> userManager, IMailService mailService)
        {
            _service = service;
            _mapper = mapper;
            _userManager = userManager;
            _mailService = mailService;
        }

        [HttpPost]
        [Route("ForgotPasswordRequest")]
        public async Task<IActionResult> ForgotPasswordAsync(EmailForToken email)
        {
            
            var random = new Random();
            string message = random.Next(1000, 9999).ToString();
            string content = $"<p>Please use the following security code for the Aduaba account : </p>" + message;

            var subject = "Security Code";

            var user = await _userManager.FindByEmailAsync(email.Email);

            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Email Not Found!" });

            await _mailService.SendEmailAsync(email.Email, subject, content);

            
            //I saved the token sent so to validate it
            user.ResetPasswordToken = message;
            await _userManager.UpdateAsync(user);

            return Ok (new Response { Status = "Success", Message = "Code sent successfully!!!" });
            
        }

        [HttpPost]
        [Route("ValidateResetToken")]
        public async Task<IActionResult> ValidateResetTokenAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (!(user.ResetPasswordToken == token))
            {
                return Ok(new Response { Status = "Failed", Message = "Invalid Token" });
            }

            return Ok(new Response { Status = "Success", Message = "Validated Successfully" });
            
        }


        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync(string email, ResetPassword model)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound();

            user.PasswordHash = new PasswordHasher<object>().HashPassword(null, model.Password);
            user.LastModifiedDate = model.LastModifiedDate;

            //because I didn't assign expiry time for the token, I ve to disable it in other not to allow the user use it again for resetting password
            user.ResetPasswordToken = string.Empty;
            
            await _userManager.UpdateAsync(user);

            return Ok(new Response { Status = "Success", Message = "Reset Successfully" });


        }

        //updating user details
        [HttpPatch]
        [Route("Edit-UsersDetails")]
        public IActionResult UpdateUser(string userId, JsonPatchDocument<UpdateUserDetails> userToUpdate)
        {
            var attemptingUser = _service.GetUser(userId);

            if (attemptingUser == null)
                return NotFound();

            var userToPatch = _mapper.Map<UpdateUserDetails>(attemptingUser);
            userToUpdate.ApplyTo(userToPatch, ModelState);

            if (!TryValidateModel(userToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(userToPatch, attemptingUser);

            _service.UpdateUser(attemptingUser);

            return NoContent();

        }
    }
}
    
