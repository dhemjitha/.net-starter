using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication6.Models;
using WebApplication6.Services;

namespace WebApplication6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserServices _services;

        public UsersController()
        {
            _services = new UserServices();
        }

        [HttpGet("{id?}")]
        public IActionResult GetAllUsers(int? id)
        {
            var users = _services.GetAllUsers();

            if (id is null)
            {
                return Ok(users);
            }

            users = users.Where(u => u.Id == id).ToList();

            return Ok(users);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email))
            {
                return BadRequest("Name and Email are required fields.");
            }

            var createdUser = _services.AddUser(user);

            return CreatedAtAction(nameof(GetAllUsers), new { id = createdUser.Id }, createdUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {

            var userToDelete = new User { Id = id };
            var deletedUser = _services.DeletableUser(userToDelete);
            if (deletedUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(deletedUser);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email))
            {
                return BadRequest("Name and Email are required fields.");
            }

            user.Id = id;
            var updatedUser = _services.UpdateUser(user);

            if (updatedUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(updatedUser);
        }
    }
}
