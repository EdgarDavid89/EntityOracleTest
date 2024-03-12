using System.ComponentModel.DataAnnotations;
using System.Net;
using DataAccess.Business;
using DataAccess.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parkings.Models;
using Parkings.Secutiry;

namespace Parkings.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication _authentication;
        private readonly IUserB _userB;

        public AuthenticationController(IAuthentication authentication,
        IUserB userB)
        {
            _authentication = authentication;
            _userB = userB;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AccessRequest request)
        {
            UserDto user = await _userB.GetUserByUserName(request.User);

            if (user == null)
            {
                return Unauthorized(new ResponseRequest
                {
                    Result = false,
                    Message = "Invalid user or password"
                });
            }

            if (!_authentication.VerifyPassword(request.Password, user.Password))
            {
                return Unauthorized(new ResponseRequest
                {
                    Result = false,
                    Message = "Invalid user or password"
                });
            }

            var token = _authentication.CreateToken(request.User);

            return Ok(new ResponseRequest
            {
                Token = token,
                RefreshToken = token,
                Result = true,
                Message = "Success"
            });
        }


        [HttpPost("user")]
        [AllowAnonymous]
        public async Task<ActionResult> CreateUser(UserDto user)
        {
            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(user, context, results, true))
            {
                return BadRequest(results.Select(r => r.ErrorMessage));
            }

            user.Password = _authentication.HasPassword(user.Password);

            var userSaved = await _userB.CreateUser(user);

            return Ok("User created");
        }
    }
}