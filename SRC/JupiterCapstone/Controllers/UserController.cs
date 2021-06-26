using AutoMapper;
using JupiterCapstone.DTO;
using JupiterCapstone.Models;
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

        private readonly IEmailSender _emailSender;

        private readonly IIdentityService _identityService;

        public UserController(IUserService service, IMapper mapper, UserManager<User> userManager, IEmailSender emailSender, IIdentityService identityService)
        {
            _service = service;
            _mapper = mapper;
            _userManager = userManager;
            _emailSender = emailSender;
            _identityService = identityService;
        }

        [HttpPost]
        [Route("ForgotPasswordRequest")]
        public async Task<IActionResult> ResetPasswordAsync(string email)
        {
            var random = new Random();
            string message = random.Next(1000, 9999).ToString();

            var subject = "RESET CODE";

            var check = await _userManager.FindByEmailAsync(email);

            if (check == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Email Not Found!" });

            await _emailSender.SendEmailAsync(email, subject, message);

            //generate a token for the user/ is true
            var token = await _identityService.GenerateToken(check);
           
            //how to return a token and code
            return Ok (token);
            //return the token with the code sent to the mail of the user, for FE to verify...my assumption
            //return StatusCode(StatusCodes.Status200OK, new ResponseCode { Token = token.ToString(), CodeSent = message });
        }

        //yet to run the code
        //The above code sent a token containing user details that will change password, so FE guys will pass in the userId from there
        [HttpPatch("{userId}")]
        public IActionResult ResetPassword(string userId, JsonPatchDocument<ResetPassword> passwordToReset)
        {
            var attemptingUser = _service.GetUser(userId);

            if (attemptingUser == null)
                return NotFound();

            var passwordToPatch = _mapper.Map<ResetPassword>(attemptingUser);
            passwordToReset.ApplyTo(passwordToPatch, ModelState);

            if (!TryValidateModel(passwordToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(passwordToPatch, attemptingUser);

            _service.UpdateUser(attemptingUser);

            return NoContent();

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
    
