using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        private readonly IUserRepository _userRepository;

        public UsersController(ILogger<UsersController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<User>> Index()
        {
            var users = await _userRepository.GetAllUsers();

            return users;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user is null)
            {
                _logger.LogDebug("Inexistent User Id.");
                return NotFound();
            }

            return Ok(user);
        }
    }
}
