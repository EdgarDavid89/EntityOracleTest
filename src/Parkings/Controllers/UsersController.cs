using Microsoft.AspNetCore.Mvc;
using DataAccess.Business;
using DataAccess.DTO;
using System.ComponentModel.DataAnnotations;

namespace Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserB _userB;

        public UsersController(IUserB userB)
        {
            _userB = userB;   
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                var result = await _userB.GetUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(decimal id)
        {
            var user = await _userB.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserDto user)
        {
            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(user, context, results, true))
            {
                return BadRequest(results.Select(r => r.ErrorMessage));
            }

            var userSaved = await _userB.CreateUser(user);

            return CreatedAtAction(nameof(GetUser), new { id = userSaved.Id }, userSaved);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParking(decimal id)
        {
            try
            {
                var parking = await _userB.DeleteUser(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(decimal id, UserDto user)
        {
            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(user, context, results, true))
            {
                return BadRequest(results.Select(r => r.ErrorMessage));
            }

            if (id != user.Id)
            {
                return BadRequest();
            }
            try
            {
                var result = await _userB.UpdateUser(user, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}