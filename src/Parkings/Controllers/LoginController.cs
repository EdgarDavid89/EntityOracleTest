using System.Net;
using Microsoft.AspNetCore.Mvc;
using Parkings.Models;
using Parkings.Secutiry;

namespace Parkings.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthentication _authentication;

        public LoginController(IAuthentication _authentication)
        {
            this._authentication = _authentication;
        }

        [HttpPost]
        public IActionResult Login([FromBody] AccessRequest request)
        {
            if (request.User == "admin" && request.Password == "admin")
            {
                var token = _authentication.CreateToken(request.User);

                return Ok(new ResponseRequest
                {
                    Token = token,
                    RefreshToken = token,
                    Result = true,
                    Message = "Success"
                });
            }

            return Unauthorized(new ResponseRequest
            {
                Result = false,
                Message = "Invalid user or password"
            });
        }
    }
}