using Core.Contracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers([Range(1, int.MaxValue)] int offset = 1, [Range(1, 10)] int limit = 2)
        {
            return Ok(_userService.GetAllUsers(offset, limit));
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([Required] string firstName, [Required] string lastName, [Required] string userName, [Required] string password)
        {
            var existingUser = await _userService.GetUserByUserName(userName);

            if (existingUser != null)
            {
                return BadRequest("User already exists.");
            }

            var createdUser = await _userService.AddUser(firstName, lastName, userName, password);

            return Ok(createdUser);
        }

        [HttpPatch]
        [Route("{userName}")]
        public async Task<ActionResult<User>> UpdateUser([Required][FromRoute] string userName, [Required] string firstName, [Required] string lastName)
        {
            var existingUser = await _userService.GetUserByUserName(userName);

            if (existingUser == null)
            {
                return NotFound();
            }

            var updatedUser = await _userService.UpdateUser(userName, firstName, lastName);
            return Ok(updatedUser);
        }

        [HttpDelete]
        [Route("{userName}")]
        public async Task<ActionResult<User>> DeleteUser([Required][FromRoute] string userName)
        {
            var existingUser = await _userService.GetUserByUserName(userName);

            if (existingUser == null)
            {
                return NotFound();
            }

            await _userService.DeleteUser(userName);

            return NoContent();
        }
    }
}